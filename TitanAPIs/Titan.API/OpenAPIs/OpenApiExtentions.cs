using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using Titan.Client.Functions;

namespace Titan.Client.Functions
{
    /// <summary>
    /// This class adds an extention method that is used by an OpenAPI to update the users JWT 
    /// when it is refreshed. HttpContext is injected via the client, with the property defined
    /// in this file, which is set in startup where ther services are created. 
    /// </summary>
    public static class OpenAPIExtentions
    {
        public static void RefreshToken(HttpResponseMessage response, HttpContext HttpContext)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)(HttpContext?.User?.Identity);

            if (response.Headers.Contains("X-Update-Token") && HttpContext != null
                && claimsIdentity != null)
            {
                Claim oldToken = claimsIdentity.FindFirst("token");

                Claim newToken = new("token", response.Headers
                    .First(header => header.Key == "X-Update-Token").Value.First());
                claimsIdentity.AddClaim(newToken);
                claimsIdentity.RemoveClaim(oldToken);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,

                    IsPersistent = true,
                    ExpiresUtc = DateTime.Parse(response.Headers
                        .First(header => header.Key == "X-Update-Token-Exp").Value.First(),
                            styles: System.Globalization.DateTimeStyles.AssumeUniversal)
                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    HttpContext.User,
                    authProperties).Wait();
            }
        }
    }
}

namespace SageAPIConnection
{
    public partial class SageAPI
    {
        internal HttpContext HttpContext { get; set; }

        // handle token refresh
        partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
        {
            OpenAPIExtentions.RefreshToken(response, HttpContext);
        }
    }
}

namespace TitanAdminConnection
{
    public partial class TitanAdmin
    {
        internal HttpContext HttpContext { get; set; }

        // handle token refresh
        partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
        {
            OpenAPIExtentions.RefreshToken(response, HttpContext);
        }
    }
}
