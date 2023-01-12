using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using TitanAuthConnection;

namespace Titan.Stock.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration config;

        internal ILogger<AccountController> logger { get; private set; }

        public AccountController(IConfiguration config, TitanAuth titanAuth, ILogger<AccountController> _logger)
        {
            this.config = config;
            TitanAuth = titanAuth;
            logger = _logger;
        }

        private TitanAuth TitanAuth { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            ViewBag.FailedLogin = false;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string Pin, string returnUrl)
        {
            // login via the API
            logger.LogInformation("Attempting to Login");

            try
            {
                var Login = await TitanAuth.PinLoginAsync(Pin);

                JwtSecurityTokenHandler tokenHandler = new();

                tokenHandler.ValidateToken(Login.Token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = false,
                    RequireSignedTokens = false,
                    SignatureValidator = delegate (string token,
                        TokenValidationParameters parameters)
                    {
                        var jwt = new JwtSecurityToken(token);

                        return jwt;
                    },
                },
                out SecurityToken validatedToken);

                ClaimsIdentity claimsIdentity =
                    new(CookieAuthenticationDefaults.AuthenticationScheme);

                // add claims returned by api to current user
                claimsIdentity.AddClaims(((JwtSecurityToken)validatedToken).Claims);
                claimsIdentity.AddClaim(new Claim("token", Login.Token));

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    ExpiresUtc = validatedToken.ValidTo
                };

                // sign in user with same timeout as token (gives client session cookie)
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

                return this.RedirectToLocal(returnUrl);
            }
            catch (Titan.OpenAPIs.ApiException e)
            {
                if (e.StatusCode == 400)
                {
                    logger.LogWarning("Login Failed");
                    ViewBag.FailedLogin = true;
                    return View();
                }
                else
                {
                    // log error
                    throw;
                }
            }
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction(nameof(Login));
        }
    }
}