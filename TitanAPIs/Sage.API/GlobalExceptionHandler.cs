using Sage.Api.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Sage.Api
{
    /// <summary>
    /// This error handler is a custom implementation of the IExceptionHandler interface for OWIN HTTP
    /// It is designed to handle exceptions thrown by an API, extract information about the error, and return it in a JSON format.
    /// The error handler uses the BuildErrorMessage() method to build a string containing information about the error,
    /// which is then included in the JSON response. It also includes a correlation ID in the response, which can be used
    /// to trace the error. In addition, the HandleAPIError() method handles nested API exceptions specifically,
    /// but it is currently commented out as this API does not consume any other API.
    /// </summary>
    public class CustomExceptionHandler : IExceptionHandler
    {
        static string CurrentAppName =
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string CurrentAPIMessage = $"{CurrentAppName} encountered an error: ";

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            // get the error from the context
            var Error = context?.Exception;

            //// check if the error is an ApiException and handle it accordingly
            //var TestError = Error;
            //do
            //{
            //    if (TestError is ApiException APIError)
            //    {
            //        return HandleAPIError(APIError);
            //    }

            //    TestError = TestError.InnerException;
            //}
            //while (TestError != null);

            // build the error message and return it to the client,
            // adding the name of the API and indenting
            context.Result = new ResponseMessageResult(new HttpResponseMessage
            {
                Content = JsonContent.Create(new ErrorViewModel
                {
                    Message = CurrentAPIMessage + Indent(BuildErrorMessage(Error)),
                    CorrelationId = context.Request.GetCorrelationId().ToString()
                }),
                StatusCode = HttpStatusCode.InternalServerError
            });

            return Task.FromResult(0);
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

                case OperationCanceledException OperationCanceledException:
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

        ///// <summary>
        ///// Handles errors that occur when calling an API.
        ///// If the error is a server error (status code 500), the method attempts to
        ///// deserialize the error response from the API and return it to the client.
        ///// Otherwise, it returns a generic error message defined by Swagger to the client.
        ///// </summary>
        ///// <param name="APIError">The error that occurred when calling the API.</param>
        ///// <returns>The error information to return to the client.</returns>
        //private ActionResult HandleAPIError(ApiException APIError)
        //{
        //    // custom handling for APIException - the first part of the message is the
        //    // error message configured in swagger

        //    var APIMessage = APIError.Message.Split("\n\n")[0];
        //    var StatusCode = APIError.StatusCode;

        //    switch (StatusCode)
        //    {
        //        case 500:
        //            // api had an internal server error

        //            // try to deserialize the error response from the API
        //            try
        //            {
        //                var APINestedError =
        //                    JsonConvert.DeserializeObject<ErrorViewModel>(APIError.Response);

        //                // set the correlation ID if it is not already set
        //                APINestedError.CorrelationId ??= HttpContext.TraceIdentifier;

        //                // return the error information to the client
        //                return Json(new ErrorViewModel()
        //                {
        //                    Message = CurrentAPIMessage + Indent(APINestedError.Message),
        //                    CorrelationId = APINestedError.CorrelationId
        //                });
        //            }
        //            catch (Exception)
        //            {
        //                // if we could not deserialize the error response, write it directly
        //                return Json(new ErrorViewModel()
        //                {
        //                    Message = CurrentAPIMessage +
        //                        Indent(APIError.Response
        //                            .Split("HEADERS\r\n=======")[0]),
        //                    CorrelationId = HttpContext.TraceIdentifier
        //                });
        //            }
        //        default:
        //            // check if the error message indicates that the API returned an unexpected status code
        //            if (APIMessage
        //                .Contains("The HTTP status code of the response was not expected"))
        //            {
        //                // api returned response code we don't recognise
        //                return Json(new ErrorViewModel()
        //                {
        //                    Message = "An API encountered an error that has not been documented: " +
        //                        ((HttpStatusCode)StatusCode).ToString() + $" ({StatusCode})",
        //                    CorrelationId = HttpContext.TraceIdentifier
        //                });
        //            }

        //            // if the error message does not indicate an unexpected status code, it is
        //            // a documented Swagger code, so pass the message on unchanged
        //            return Json(new ErrorViewModel()
        //            {
        //                Message = APIMessage,
        //                CorrelationId = HttpContext.TraceIdentifier
        //            });
        //    }
        //}
    }
}
