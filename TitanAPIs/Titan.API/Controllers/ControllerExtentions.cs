using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Titan.API.Controllers
{
    public static class ControllerExtentions
    {
        /// <summary>
        /// Extension method for the HttpContext class that returns the display name of a user.
        /// The method retrieves the user's name claim from the Claims property of the User object,
        /// and returns the value of the claim if it exists. If the claim does not exist,
        /// the method returns "Unknown" instead.
        /// </summary>
        /// <returns>A string containing the display name of the user.</returns>
        public static string GetUserDisplayName(this HttpContext HttpContext)
        {
            // Get the user's name claim from the Claims property of the User object.
            // If the claim does not exist, return "Unknown".
            return HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "Unknown";
        }
    }
}
