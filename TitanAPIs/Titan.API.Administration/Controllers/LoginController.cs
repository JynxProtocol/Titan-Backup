using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using Titan.Data;
using Titan.Functions;
using Titan.Models.Account;
using Titan.Models.DBUser;


namespace Titan.API.Administration.Controllers
{
    /// <summary>
    /// Login endpoints are used to authenticate users and return JWTs with user permissions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public LoginController(IConfiguration configuration, TitanAuthContext _db)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            TitanAuth = _db;
            Configuration = configuration;
        }
        internal IConfiguration Configuration { get; }
        internal TitanAuthContext TitanAuth { get; }

        // POST api/<LoginController>
        /// <summary>
        /// Log in a user using a username and password
        /// </summary>
        /// <param name="request">The username and password</param>
        /// <returns>
        /// The generated JWT, expiration and the list of claims given to the token
        /// </returns>
        /// <response code="200">Returns the LoginResponse</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid password)
        /// </response>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<LoginResponse> Login(LoginRequest request)
        {
            var user = TitanAuth.Users.FirstOrDefault(usr => usr.UsrName == request.Username);

            if (user == null || request.Password == null || request.Username == null)
            {
                //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"A login attempt failed")
                //{
                //    AdditionalInfo = new Dictionary<string, object> {
                //        { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
                //        { "User", request.Username }
                //    }
                //});

                return BadRequest();
            }

            if (user.UsrEnabled == false)
            {
                //Security.Log(new SecurityLogDetails(HttpContext, $"A login attempt was made " +
                //    $"against disabled user {request.Username}")
                //{
                //    AdditionalInfo = new Dictionary<string, object> {
                //        { "Identity", (ClaimsIdentity)HttpContext.User?.Identity }
                //    }
                //});
                return BadRequest();
            }

            // verify password
            var hmac = MyHmac.GenerateSaltedHash(Encoding.UTF8.GetBytes(request.Password),
                Encoding.UTF8.GetBytes(user.SALT));
            string myHASH = Convert.ToBase64String(hmac);

            if (user.HASH != myHASH)
            {
                //Security.Log(new SecurityLogDetails(HttpContext, $"A login attempt for user " +
                //    $"{request.Username} failed due to a bad password")
                //{
                //    AdditionalInfo = new Dictionary<string, object> {
                //        { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
                //        { "User", user.ToClientUser() }
                //    }
                //});
                return BadRequest(); // Username or Password do not match
            }

            // add authenticated fields
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UsrName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UsrID.ToString()),
                new Claim("OriginalCreateDate", DateTime.UtcNow.ToString())
            };

            var userFeatures = (from usr in TitanAuth.Users
                                join roleMapping in TitanAuth.UserRoleMappings
                                on usr.UsrID equals roleMapping.UsrID
                                join role in TitanAuth.Roles
                                on roleMapping.RoleID equals role.ID
                                join featureMapping in TitanAuth.RoleFeatureMappings
                                on role.ID equals featureMapping.RoleID
                                join feature in TitanAuth.Features
                                on featureMapping.FeatureID equals feature.ID
                                where usr.UsrID == user.UsrID
                                select feature.FeatureName).Distinct().ToArray();

            claims.Add(new Claim("Features",
                System.Text.Json.JsonSerializer.Serialize(userFeatures)));


            // sign new JWT with authed fields
            var token = new JwtSecurityToken(
                Configuration["TokenValidIssuer"],
                Configuration["TokenValidAudience"],
                claims,
                signingCredentials: Startup.JWTCreds,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(Configuration["TokenTimeoutMins"])));

            APIAuthFunctions.SecurityLog(TitanAuth, user.UsrName);

            var claimsDict = new Dictionary<string, string>();
            foreach (Claim claim in claims)
            {
                claimsDict[claim.Type] = claim.Value;
            }

            //Security.LogVerbose(
            //    new SecurityLogDetails(HttpContext, $"Logged in user {request.Username}")
            //    {
            //        AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user.ToClientUser() }
            //        }
            //    });

            //Security.Log(new SecurityLogDetails(HttpContext, $"Server generated JWT " +
            //    $"for user {request.Username}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user.ToClientUser() },
            //            { "Token", new JwtSecurityTokenHandler().WriteToken(token) },
            //            { "Claims", claims },
            //            { "Expiry", token.ValidTo }
            //        }
            //});

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Claims = claimsDict
            };
        }

        // POST api/<LoginController>/Pin
        /// <summary>
        /// Log in a user using a pin code
        /// </summary>
        /// <param name="Pin">The pin of the user to log in</param>
        /// <returns>
        /// The generated JWT, expiration and the list of claims given to the token.
        /// Only barcode features are included
        /// </returns>
        /// <response code="200">Returns the LoginResponse</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid pin)
        /// </response>
        [HttpPost("Pin")]
        [AllowAnonymous]
        public ActionResult<LoginResponse> PinLogin([FromBody] string Pin)
        {
            var user = TitanAuth.Users.FirstOrDefault(usr => usr.UsrPin == Pin);

            if (user == null)
            {
                //Security.LogVerbose(
                //    new SecurityLogDetails(HttpContext, $"A pin login attempt failed")
                //    {
                //        AdditionalInfo = new Dictionary<string, object> {
                //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity }
                //        }
                //    });

                return BadRequest();
            }

            if (user.UsrEnabled == false)
            {
                //Security.Log(new SecurityLogDetails(HttpContext, $"A pin login attempt was made " +
                //    $"against disabled user {user.UsrName}")
                //{
                //    AdditionalInfo = new Dictionary<string, object> {
                //        { "Identity", (ClaimsIdentity)HttpContext.User?.Identity }
                //    }
                //});
                return BadRequest();
            }

            // add authenticated fields
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UsrName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, "PinAuth"),
                new Claim("OriginalCreateDate", DateTime.UtcNow.ToString())
            };

            var userFeatures = (from usr in TitanAuth.Users
                                join roleMapping in TitanAuth.UserRoleMappings
                                on usr.UsrID equals roleMapping.UsrID
                                join role in TitanAuth.Roles
                                on roleMapping.RoleID equals role.ID
                                join featureMapping in TitanAuth.RoleFeatureMappings
                                on role.ID equals featureMapping.RoleID
                                join feature in TitanAuth.Features
                                on featureMapping.FeatureID equals feature.ID
                                where usr.UsrID == user.UsrID && feature.FeatureArea == "Barcode"
                                select feature.FeatureName).Distinct().ToArray();

            claims.Add(new Claim("Features",
                System.Text.Json.JsonSerializer.Serialize(userFeatures)));


            // sign new JWT with authed fields
            var token = new JwtSecurityToken(
                Configuration["TokenValidIssuer"],
                Configuration["TokenValidAudience"],
                claims,
                signingCredentials: Startup.JWTCreds,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(Configuration["TokenTimeoutMins"])));

            APIAuthFunctions.SecurityLog(TitanAuth, user.UsrName);

            var claimsDict = new Dictionary<string, string>();
            foreach (Claim claim in claims)
            {
                claimsDict[claim.Type] = claim.Value;
            }

            //Security.LogVerbose(
            //    new SecurityLogDetails(HttpContext, $"Logged in user {user.UsrName} with pin")
            //    {
            //        AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user.ToClientUser() }
            //        }
            //    });

            //Security.Log(new SecurityLogDetails(HttpContext, $"Server generated JWT " +
            //    $"for user {user.UsrName} with pin")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user.ToClientUser() },
            //            { "Token", new JwtSecurityTokenHandler().WriteToken(token) },
            //            { "Claims", claims },
            //            { "Expiry", token.ValidTo }
            //        }
            //});

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Claims = claimsDict
            };
        }

        // GET api/<LoginController>/ValidToken
        /// <summary>
        /// Updates a users JWT to persist their session, keeping thier claims
        /// </summary>
        /// <returns>
        /// The updated JWT, new expiration and the list of claims given to the token
        /// </returns>
        /// <response code="200">Returns the updated JWT</response>
        /// <response code="400">The request is invalid</response>
        [HttpGet("/api/UpdateJWT")]
        [Authorize]
        public ActionResult<LoginResponse> UpdateJWT()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)HttpContext.User.Identity;

            try
            {
                return UpdateJWTForUser(
                    claimsIdentity,
                    TitanAuth,
                    Configuration["TokenRefreshWindowMins"],
                    Configuration["TokenMaxLifetimeMins"],
                    Configuration["TokenValidIssuer"],
                    Configuration["TokenValidAudience"],
                    Configuration["TokenTimeoutMins"]
                    );
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        internal static LoginResponse UpdateJWTForUser(ClaimsIdentity ClaimsIdentity,
            TitanAuthContext TitanAuth, string TokenRefreshWindow, string TokenMaxLifetime,
            string Issuer, string Audience, string Timeout)
        {
            string UsrID = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //do not re-issue AuthTokens
            if (UsrID == "AuthToken" || UsrID == null)
            {
                throw new SecurityException();
            }

            DBUser user = new();

            // get authorised user from TitanAuth
            user = TitanAuth.Users.FirstOrDefault(User => User.UsrID.ToString() == UsrID);

            if (user == null)
            {
                //Error.Log(
                //    new ErrorLogDetails(
                //    HttpContext,
                //    $"User with valid UsrID Claim could not be found in database")
                //    {
                //        AdditionalInfo = new Dictionary<string, object> {
                //        { "UsrID", UsrID},
                //        { "Identity", claimsIdentity},
                //        { "Token", HttpContext.GetTokenAsync("access_token").Result}
                //        }
                //    });

                //Security.Log(
                //    new SecurityLogDetails(
                //        HttpContext,
                //        $"User with valid UsrID Claim could not be found in database")
                //    {
                //        AdditionalInfo = new Dictionary<string, object> {
                //            { "UsrID", UsrID},
                //            { "Identity", claimsIdentity},
                //            { "Token", HttpContext.GetTokenAsync("access_token").Result}
                //        }
                //    });

                throw new SecurityException();
            }

            if (user.UsrEnabled == false)
            {
                //Security.LogWarning(
                //    new SecurityLogDetails(
                //        HttpContext,
                //        $"Disabled user fails to generate a new JWT")
                //    {
                //        AdditionalInfo = new Dictionary<string, object> {
                //            { "Identity", claimsIdentity},
                //            { "Token", HttpContext.GetTokenAsync("access_token").Result}
                //        }
                //    });

                throw new SecurityException();
            }

            // rolling session by re-issuing tokens
            DateTimeOffset tokenExpiry = DateTimeOffset.FromUnixTimeSeconds(
                    int.Parse(ClaimsIdentity.FindFirst(JwtRegisteredClaimNames.Exp).Value)
                );

            if (tokenExpiry - DateTime.UtcNow >=
                TimeSpan.FromMinutes(int.Parse(TokenRefreshWindow)))
            {
                throw new SecurityException();
            }

            DateTime originalCreateDate =
                    DateTime.Parse(ClaimsIdentity.FindFirst("OriginalCreateDate").Value);

            // maximum session lifetime of 18 hours
            if (DateTime.UtcNow - originalCreateDate >=
                TimeSpan.FromMinutes(int.Parse(TokenMaxLifetime)))
            {
                throw new SecurityException();
            }

            // add authenticated fields
            var claims = new List<Claim>();
            claims.AddRange(ClaimsIdentity.Claims.Where(c =>
                !(new string[] {
                            "Features",
                            JwtRegisteredClaimNames.Exp,
                            JwtRegisteredClaimNames.Iss,
                            JwtRegisteredClaimNames.Aud
                }).Contains(c.Type)));

            var tokenFeatures = (from usr in TitanAuth.Users
                                 join roleMapping in TitanAuth.UserRoleMappings
                                 on usr.UsrID equals roleMapping.UsrID
                                 join role in TitanAuth.Roles
                                 on roleMapping.RoleID equals role.ID
                                 join featureMapping in TitanAuth.RoleFeatureMappings
                                 on role.ID equals featureMapping.RoleID
                                 join feature in TitanAuth.Features
                                 on featureMapping.FeatureID equals feature.ID
                                 where usr.UsrID == user.UsrID
                                 select feature.FeatureName).Distinct().ToArray();

            claims.Add(new Claim("Features",
                System.Text.Json.JsonSerializer.Serialize(tokenFeatures)));

            // update expiration, keep claims
            var updatedToken = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                signingCredentials: Startup.JWTCreds,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(Timeout)));

            //Security.Log(
            //new SecurityLogDetails(
            //    HttpContext,
            //    $"Server generates a new JWT for user '{user.UsrName}' " +
            //    $"with expiry '{updatedToken.ValidTo}'")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", claimsIdentity},
            //            { "OldToken", HttpContext.GetTokenAsync("access_token").Result},
            //            { "NewToken", new JwtSecurityTokenHandler()
            //                .WriteToken(updatedToken)}
            //}
            //});

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(updatedToken),
                Expiration = updatedToken.ValidTo,
                Claims = updatedToken.Claims
                    .ToDictionary(Claim => Claim.Type, Claim => Claim.Value)
            };
        }
    }
}
