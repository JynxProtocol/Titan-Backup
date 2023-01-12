using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TitanAdminConnection;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Titan.Filters
{
    public class FeatureAttribute : TypeFilterAttribute
    {
        public FeatureAttribute(string Feature = "") : base(typeof(FeatureFilter))
        {
            Arguments = new object[] { Feature };
        }
    }

    // Custom authorisation filter used to check user access privilages and determine authorisation using 'Features'
    public class FeatureFilter : IAsyncAuthorizationFilter
    {
        readonly string _Feature;

        public IConfiguration Configuration { get; }
        public TitanAdmin TitanAdmin { get; }

        public FeatureFilter(IConfiguration configuration, TitanAdmin titanAdmin, string Feature = "")
        {
            _Feature = Feature;
            Configuration = configuration;
            TitanAdmin = titanAdmin;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)context.HttpContext.User.Identity;

            if (!claimsIdentity.IsAuthenticated)
            {
                //Security.LogVerbose(
                //    new SecurityLogDetails(
                //        context.HttpContext,
                //        $"Un-authenticated user attempts to access protected area '{context.ActionDescriptor.DisplayName}'")
                //    {
                //        AdditionalInfo = new Dictionary<string, object> {
                //            { "Action", context.ActionDescriptor.DisplayName },
                //            { "Identity", claimsIdentity},
                //            { "Token", context.HttpContext.GetTokenAsync("access_token").Result}
                //        }
                //    });
                context.Result = new UnauthorizedResult();
                return;
            }

            // get authorised user id from token
            if (!claimsIdentity.HasClaim(Claim => Claim.Type == ClaimTypes.NameIdentifier))
            {
                //Error.Log(
                //    new ErrorLogDetails(
                //    context.HttpContext,
                //    $"Could not get UsrId from valid JWT")
                //    {
                //        AdditionalInfo = new Dictionary<string, object> {
                //        { "Token", context.HttpContext.GetTokenAsync("access_token").Result}
                //        }
                //    });

                //Security.Log(
                //    new SecurityLogDetails(
                //    context.HttpContext,
                //    $"Could not get UsrId from valid JWT")
                //    {
                //        AdditionalInfo = new Dictionary<string, object> {
                //        { "Token", context.HttpContext.GetTokenAsync("access_token").Result}
                //        }
                //    });

                context.Result = new UnauthorizedResult();
                return;
            }

            string UsrID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            // if feature was not specified, we just want any authed user
            if (_Feature == null || _Feature == "")
            {
                //Security.LogVerbose(
                //    new SecurityLogDetails(
                //        context.HttpContext,
                //        $"User succesfully accesses protected area '{context.ActionDescriptor.DisplayName}'")
                //    {
                //        AdditionalInfo = new Dictionary<string, object> {
                //            { "Action", context.ActionDescriptor.DisplayName },
                //            { "Identity", claimsIdentity},
                //            { "Token", context.HttpContext.GetTokenAsync("access_token").Result}
                //        }
                //    });
                return;
            }

            string featuresJSON = claimsIdentity.FindFirst(Claim => Claim.Type == "Features")?.Value;
            var userFeatures = (featuresJSON != null) ? System.Text.Json.JsonSerializer.Deserialize<string[]>(featuresJSON) : Array.Empty<string>();

            if (!userFeatures.Contains(_Feature))
            {
                //Security.LogVerbose(
                //    new SecurityLogDetails(
                //        context.HttpContext,
                //        $"User is denied access to protected area '{context.ActionDescriptor.DisplayName}' with feature '{_Feature}'")
                //    {
                //        AdditionalInfo = new Dictionary<string, object> {
                //            { "Action", context.ActionDescriptor.DisplayName },
                //            { "Feature", _Feature },
                //            { "Identity", claimsIdentity},
                //            { "Token", context.HttpContext.GetTokenAsync("access_token").Result}
                //        }
                //    });

                context.Result = new ForbidResult();
                return;
            }
            else if (!(UsrID == "AuthToken"))
            {
                // rolling session by re-issuing tokens
                DateTimeOffset tokenExpiry = DateTimeOffset.FromUnixTimeSeconds(int.Parse(claimsIdentity.FindFirst(JwtRegisteredClaimNames.Exp).Value));

                if (tokenExpiry - DateTime.UtcNow < TimeSpan.FromMinutes(int.Parse(Configuration["TokenRefreshWindowMins"])))
                {
                    DateTime originalCreateDate = DateTime.Parse(claimsIdentity.FindFirst("OriginalCreateDate").Value);
                    // maximum session lifetime of 18 hours
                    if (DateTime.UtcNow - originalCreateDate < TimeSpan.FromMinutes(int.Parse(Configuration["TokenMaxLifetimeMins"])))
                    {
                        try
                        {
                            var UpdateResult = await TitanAdmin.UpdateJWTAsync();

                            // return token via custom headers
                            context.HttpContext.Response.Headers.Add(
                                new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("X-Update-Token", UpdateResult.Token)
                                );
                            context.HttpContext.Response.Headers.Add(
                                new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("X-Update-Token-Exp", UpdateResult.Expiration.ToString())
                                );
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }

            //Security.LogVerbose(
            //    new SecurityLogDetails(
            //        context.HttpContext,
            //        $"User succesfully accesses protected area '{context.ActionDescriptor.DisplayName}' with feature '{_Feature}'")
            //    {
            //        AdditionalInfo = new Dictionary<string, object> {
            //            { "Action", context.ActionDescriptor.DisplayName },
            //            { "Feature", _Feature },
            //            { "Identity", claimsIdentity},
            //            { "Token", context.HttpContext.GetTokenAsync("access_token").Result}
            //        }
            //    });
            return;

        }
    }
}
