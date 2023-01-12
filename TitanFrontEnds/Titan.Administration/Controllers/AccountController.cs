using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Titan.Administration.Functions;
using TitanAPIAdminConnection;

namespace Titan.Administration.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration config;

        public AccountController(IConfiguration config, TitanAdmin titanAdmin)
        {
            this.config = config;
            TitanAdmin = titanAdmin;
        }

        private TitanAdmin TitanAdmin { get; set; }

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
        public async Task<ActionResult> Login(string User, String Password, string returnUrl)
        {
            try
            {
                LoginResponse loginResponse = await TitanAdmin.LoginAsync(new LoginRequest { Username = User, Password = Password });

                JwtSecurityTokenHandler tokenHandler = new();

                tokenHandler.ValidateToken(loginResponse.Token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = false,
                    RequireSignedTokens = false,
                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JwtSecurityToken(token);

                        return jwt;
                    },
                },
                out SecurityToken validatedToken);

                ClaimsIdentity claimsIdentity = new(CookieAuthenticationDefaults.AuthenticationScheme);

                // add claims returned by api to current user
                claimsIdentity.AddClaims(((JwtSecurityToken)validatedToken).Claims);
                claimsIdentity.AddClaim(new Claim("token", loginResponse.Token));

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
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return this.RedirectToLocal(returnUrl);
            }
            catch (Titan.OpenAPIs.ApiException e)
            {
                if (e.StatusCode == 400)
                {
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

        [HttpGet]
        public async Task<ActionResult> MyAccount()
        {
            // gets current user from API
            var CurrentUser = await TitanAdmin.GetCurrentUserAsync();

            return View(CurrentUser);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordRequest()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPasswordRequest(string myEmailAddress)
        {
            await TitanAdmin.StartPasswordResetAsync(myEmailAddress);

            return RedirectToAction(nameof(AccountController.Login), typeof(AccountController).ControllerName());
        }

        public ActionResult UpdatePassword(string token)
        {
            return View(token);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(string password, string token)
        {
            await TitanAdmin.ResetPasswordUsingTokenAsync(new TokenChangePasswordRequest
            {
                Password = password,
                Token = token
            });

            // sign out user as precaution
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(string username, string password, string newPassword, string newPasswordC)
        {
            // validate password
            if (!(newPassword == newPasswordC))
            {
                return View();
            }

            await TitanAdmin.ChangePasswordAsync(new ChangePasswordRequest
            {
                Password = password,
                NewPassword = newPassword,
                Username = username
            });

            // force user to sign in again
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction(nameof(Login));
        }
    }
}