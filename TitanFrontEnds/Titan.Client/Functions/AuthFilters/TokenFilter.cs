using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Titan.Filters
{
    // Custom authorisation filter used to check the user has a valid token client side, and handle logouts when necessary
    public class TokenFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // bypass requiring a token if the method is marked AllowAnonyomus (login, etc.)
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                if (controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Where(attribute => attribute.GetType() == typeof(AllowAnonymousAttribute)).Any())
                {
                    return;
                }
            }

            string token = ((ClaimsIdentity)context.HttpContext.User.Identity)?.FindFirst("token")?.Value;

            // sign out user if the don't have a token (case: signed in incorrectly)
            if (token == null)
            {
                context.Result = new ChallengeResult();
                AuthenticationHttpContextExtensions.SignOutAsync(context.HttpContext);
                return;
            }

            JwtSecurityTokenHandler tokenHandler = new();

            // attempt to validate token lifetime without signing key
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = true,
                    RequireSignedTokens = false,
                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JwtSecurityToken(token);

                        return jwt;
                    },
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken validatedToken);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // sign the user out (case: user token expired due to inactivity)
                context.Result = new ChallengeResult();
                AuthenticationHttpContextExtensions.SignOutAsync(context.HttpContext);
                return;
            }

            // allow access by default
        }
    }
}
