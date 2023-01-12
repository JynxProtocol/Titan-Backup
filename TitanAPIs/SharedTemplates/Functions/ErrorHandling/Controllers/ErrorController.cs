using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Authorization;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Diagnostics;
using TitanLogging;

namespace TitanErrorHandling.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {

        public ErrorController()
        {
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public ActionResult<ErrorViewModel> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            ErrorLogDetails logdetails;
            if (context.Error != null)
            {
                logdetails = new ErrorLogDetails(HttpContext, context.Error);
            }
            else
            {
                logdetails = new ErrorLogDetails(HttpContext, "An unknown error occurred");
            }

            //if (context.Error is CriticalException)
            //{
            //    logdetails.Message = context.Error.Message;
            //    global::TitanLogging.Error.LogFatal(logdetails);

            //    return View("ErrorPage", new ErrorViewModel
            //    {
            //        Message = $"Critical error occurred (IT may need to speak to you, keep the reference handy): {context.Error.Message}",
            //        CorrelationId = TitanLogging.TitanLogging.GetCorrelationID(HttpContext)
            //    });
            //}
            //else if (context.Error is APIException)
            //{
            //    logdetails.Message = $"The API Request to {((APIException)context.Error).URL} returned an error: {context.Error.Message}";
            //    logdetails.CorrelationId = ((APIException)context.Error).CorrelationId;

            //    return View("ErrorPage", new ErrorViewModel
            //    {
            //        Message = context.Error.Message,
            //        CorrelationId = logdetails.CorrelationId
            //    });
            //}

            global::TitanLogging.Error.Log(logdetails);

            return View("ErrorPage", new ErrorViewModel
            {
                Message = logdetails.Message,
                CorrelationId = logdetails.CorrelationId
            });
        }
    }
}
