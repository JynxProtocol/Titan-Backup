using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Web;
using Titan.Administration.Models;
using Titan.OpenAPIs;

namespace Titan.Administration.Controllers
{
    /// <summary>
    /// This controller is responsible for handling errors that occur in the application.
    /// It uses the BuildErrorMessage() method to build a string containing information
    /// about the error, and it returns this information in either a JSON response or
    /// an HTML page, depending on whether the request was an AJAX request or not.
    /// It also includes a correlation ID in the response, which can be used
    /// to trace the error. In addition, the HandleAPIError() method handles nested 
    /// API exceptions specifically, and the controller also provides custom handling for certain 
    /// types of exceptions such as HttpRequestException, thrown when failing to connect to an API.
    /// </summary>
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        public ErrorController(HtmlSanitizer htmlSanitizer)
        {
            HtmlSanitizer = htmlSanitizer;
        }

        private HtmlSanitizer HtmlSanitizer { get; }

        /// <summary>
        /// A delegate that determines how to return the error information to the client.
        /// If the request is an AJAX request, the error information will be returned as JSON.
        /// Otherwise, it will be returned as an HTML page.
        /// </summary>
        private delegate ActionResult ReturnFunction(ErrorViewModel error);
        private ReturnFunction ReturnError =>
            HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest" ?
                (error) => Json(error) :
                (error) => View("ErrorPage", error);

        /// <summary>
        /// Handles errors that occur in the application.
        /// </summary>
        /// <returns>The error information, either as a JSON response or an HTML page.</returns>
        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public ActionResult<ErrorViewModel> Error()
        {
            // get the error from the context
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var Error = context?.Error;

            // check if the error is an ApiException and handle it accordingly
            var TestError = Error;
            do
            {
                if (TestError is ApiException APIError)
                {
                    return HandleAPIError(APIError);
                }

                TestError = TestError.InnerException;
            }
            while (TestError != null);

            if (HttpContext.Response.StatusCode == 403)
            {
                return View("Forbidden");
            }

            // build the error message and return it to the client
            return ReturnError(new ErrorViewModel
            {
                Message = HtmlSanitizer.Sanitize(BuildErrorMessage(Error)),
                CorrelationId = HttpContext.TraceIdentifier
            });
        }

        /// <summary>
        /// Surrounds the given string in a div tag.
        /// This is used to format the error message for display in an HTML page.
        /// </summary>
        private static string Indent(string source)
        {
            return "<div>" + source + "</div>";
        }

        /// <summary>
        /// This method builds a string containing information about the error that was thrown.
        /// It checks the type of the error and handles it accordingly, including handling
        /// specific types of exceptions and aggregate exceptions.
        /// </summary>
        /// <param name="Error">The error that was thrown.</param>
        /// <returns>A string containing information about the error.</returns>
        private string BuildErrorMessage(Exception Error)
        {
            switch (Error)
            {
                case HttpRequestException { InnerException: SocketException } HttpSocketError:
                    // custom handling for exception generated when we fail to connect to an API

                    return $"Could not connect to an API: " +
                        Indent(HttpUtility.HtmlEncode(HttpSocketError.Message));

                case AggregateException ErrorParent:
                    // if exception is aggregate exception (thrown by threads/tasks),
                    // extract the wrapped exceptions

                    string AggregateMessage = "";
                    foreach (Exception WrappedError in ErrorParent.Flatten().InnerExceptions)
                    {
                        // build a string containing information about each wrapped exception
                        AggregateMessage += BuildErrorMessage(WrappedError);
                    }

                    // return a message indicating that the error(s) occurred in a thread,
                    // along with the string containing information about the wrapped exceptions
                    return "Error(s) occured in a thread:" + Indent(AggregateMessage);

                case OperationCanceledException:
                    // custom handling for OperationCanceledExceptions,
                    // they occur in threads when they are canceled
                    // skip them since they don't add useful info

                    // return the error message, HTML-encoded to prevent against
                    // potential cross-site scripting attacks
                    return HttpUtility.HtmlEncode(Error.Message);

                case null:
                    // we could not get the error from error context

                    // return a default message indicating that an error occurred
                    // but it could not be identified
                    return "An error occurred, but could not be identified";

                default:
                    // default case for standard exceptions

                    if (Error.InnerException != null)
                    {
                        // if the error has an inner exception, recursively
                        // build a string containing information about the inner exception
                        return HttpUtility.HtmlEncode(Error.Message) +
                            Indent(BuildErrorMessage(Error.InnerException));
                    }

                    // if the error doesn't have an inner exception,
                    // return its message, HTML-encoded to prevent against
                    // potential cross-site scripting attacks
                    return HttpUtility.HtmlEncode(Error.Message);
            }
        }

        /// <summary>
        /// Handles errors that occur when calling an API.
        /// If the error is a server error (status code 500), the method attempts to
        /// deserialize the error response from the API and return it to the client.
        /// Otherwise, it returns a generic error message defined by Swagger to the client.
        /// </summary>
        /// <param name="APIError">The error that occurred when calling the API.</param>
        /// <returns>The error information to return to the client.</returns>
        private ActionResult HandleAPIError(ApiException APIError)
        {
            // custom handling for APIException - the first part of the message is the
            // error message configured in swagger

            var APIMessage = APIError.Message.Split("\n\n")[0];
            var StatusCode = APIError.StatusCode;

            if (HttpContext.Response.StatusCode == 403)
            {
                return View("Forbidden");
            }

            switch (StatusCode)
            {
                case 500:
                    // api had an internal server error

                    // try to deserialize the error response from the API
                    try
                    {
                        var APINestedError =
                            JsonConvert.DeserializeObject<ErrorViewModel>(APIError.Response);

                        // set the correlation ID if it is not already set
                        APINestedError.CorrelationId ??= HttpContext.TraceIdentifier;

                        // return the error information to the client
                        return ReturnError(new ErrorViewModel()
                        {
                            Message = HtmlSanitizer.Sanitize(APINestedError.Message),
                            CorrelationId = APINestedError.CorrelationId
                        });
                    }
                    catch (Exception)
                    {
                        // if we could not deserialize the error response, write it directly
                        return ReturnError(new ErrorViewModel()
                        {
                            Message = "An API encountered an error: " +
                                Indent(
                                    HtmlSanitizer.Sanitize(
                                        APIError.Response
                                            .Split("HEADERS\r\n=======")[0])),
                            CorrelationId = HttpContext.TraceIdentifier
                        });
                    }
                case 403:
                    return View("Forbidden");
                default:
                    // check if the error message indicates that the API returned an unexpected status code
                    if (APIMessage
                        .Contains("The HTTP status code of the response was not expected"))
                    {
                        // api returned response code we don't recognise
                        return ReturnError(new ErrorViewModel()
                        {
                            Message = "An API encountered an error that has not been documented: " +
                                ((HttpStatusCode)StatusCode).ToString() + $" ({StatusCode})",
                            CorrelationId = HttpContext.TraceIdentifier
                        });
                    }

                    // if the error message does not indicate an unexpected status code, it is
                    // a documented Swagger code, so the message is human-readable
                    return ReturnError(new ErrorViewModel()
                    {
                        Message = HtmlSanitizer.Sanitize(APIMessage),
                        CorrelationId = HttpContext.TraceIdentifier
                    });
            }
        }
    }
}