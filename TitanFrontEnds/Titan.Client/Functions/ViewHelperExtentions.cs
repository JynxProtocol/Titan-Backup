using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TitanAPIAdminConnection;

namespace Titan.Client.Functions
{
    public static class ViewHelperExtentions
    {
        /// <summary>
        /// Returns the current URL of the request being handled. This can be used to return 
        /// to pages after completing an action, using a returnURL
        /// </summary>
        public static string CurrentUrl(this HttpContext Context)
        {
            return Context.Request.PathBase + Context.Request.Path;
        }

        /// <summary>
        /// Returns a boolean value indicating whether the current user has 
        /// the specified permission, checking against TitanAdmin
        /// </summary>
        /// // IMPROVEMENT: Move away from using this method, and toward using the JWT
        public static async Task<bool> UserHasPermission(this TitanAdmin TitanAdmin,
            string Permission)
        {
            try
            {
                return await TitanAdmin.UserHasPermAsync("current", Permission);
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a boolean value indicating whether the current user has 
        /// the specified permission, checking against the list of claims in the users JWT
        /// </summary>
        public static async Task<bool> UserHasPermission(this ClaimsPrincipal User,
            string Permission)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<string>>(
                        User.Claims.SingleOrDefault(claim => claim.Type == "Features").Value
                    ).Contains(Permission);
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
