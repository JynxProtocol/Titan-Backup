using Microsoft.AspNetCore.Mvc;
using System;

namespace Titan.Client.Controllers
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Extension method that returns the name of a controller.
        /// The method removes the "Controller" suffix from the type name if it exists.
        /// </summary>
        /// <param name="controllerType">The Type on which the method is called.</param>
        /// <returns>A string containing the name of the controller.</returns>
        public static string ControllerName(this Type controllerType)
        {
            // Get the base type of the controller.
            Type baseType = typeof(Controller);

            // Check if the controller type derives from the Controller base type.
            if (baseType.IsAssignableFrom(controllerType))
            {
                // Get the index of the last "Controller" substring in the type name.
                int lastControllerIndex = controllerType.Name.LastIndexOf("Controller");

                // If the "Controller" substring exists, remove it from the type name.
                if (lastControllerIndex > 0)
                {
                    return controllerType.Name[..lastControllerIndex];
                }
            }

            // If the controller type does not derive from the Controller base type
            // or the "Controller" substring does not exist, return the original type name.
            return controllerType.Name;
        }

        /// <summary>
        /// Extension method for the Controller class that redirects the user to
        /// a specified URL, but includes checks to prevent attacks by modifying
        /// the returnUrl parameter.
        /// </summary>
        /// <param name="Controller">The Controller object to excecute the redirect on.</param>
        /// <param name="returnUrl">The URL to redirect the user to.</param>
        /// <returns>An ActionResult object that redirects the user to the specified URL.</returns>
        public static ActionResult RedirectToLocal(this Controller Controller, string returnUrl)
        {
            // If the returnUrl is a local URL, redirect the user to that URL.
            if (Controller.Url.IsLocalUrl(returnUrl))
            {
                return Controller.Redirect(returnUrl);
            }
            // If the returnUrl is not a local URL, but it is a well-formed URI string,
            // check if the PathAndQuery of the URI is a local URL.
            // If it is, redirect the user to that URL.
            else if (Uri.IsWellFormedUriString(returnUrl, UriKind.RelativeOrAbsolute))
            {
                var URL = new Uri(returnUrl);

                if (Controller.Url.IsLocalUrl(URL.PathAndQuery))
                {
                    return Controller.Redirect(URL.PathAndQuery);
                }
            }

            // If the returnUrl is not a local URL and it is not a well-formed URI string,
            // redirect the user to the default home page.
            return Controller.RedirectToAction("", "Home");
        }
    }
}