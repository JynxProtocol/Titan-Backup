using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static ConsumeAPI.APIManager;

namespace Titan.Functions
{
    public class ClientAuthFunctions
    {
        /// <summary>Function <c>HasPermission</c> requests the API whether the current user (found via application context) has access to the given Feature</summary>
        /// <param name="permission">The Feature to query access to</param>
        public static bool HasPermission(string permission, ConsumeAPI.APIManager AuthAPIResult)
        {
            try
            {
                HttpContext HttpContext = new HttpContextAccessor().HttpContext;
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)(HttpContext.User.Identity);

                var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                ObjectAPIResult<bool> HasPermission = AuthAPIResult.Recieve<bool>($"Users/{userID}/Permission/{permission}", "GET");

                if (!HasPermission.Success)
                {
                    return false;
                }

                return HasPermission.Result;
            }
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            {
                return false;
            }
            
        }
    }
}
