using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Titan.Data;
using Titan.Filters;
using Titan.Functions;
using Titan.Models.Account;

namespace Titan.API.Administration.Controllers
{
    /// <summary>
    /// Password endpoints are used manage user and admin passord resets
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PasswordController : ControllerBase
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public PasswordController(TitanAuthContext _db, SmtpClient smtpClient)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            TitanAuth = _db;
            SmtpClient = smtpClient;
        }

        internal TitanAuthContext TitanAuth { get; private set; }
        internal SmtpClient SmtpClient { get; private set; }

        // DELETE api/<PasswordController>/5
        /// <summary>
        /// Initiate a password reset on a user as admin
        /// </summary>
        /// <param name="id">The user id to initiate a password reset on</param>
        /// <response code="200">Successful reset request</response>
        /// <response code="400">
        /// The user id is invalid
        /// </response>
        [HttpDelete("{id}")]
        [Feature("AdminPasswordReset")]
        public ActionResult AdminPasswordReset(int id)
        {
            var user = TitanAuth.Users.FirstOrDefault(usr => usr.UsrID == id);

            if (user == null)
            {
                return BadRequest();
            }

            // generate new random password
            var hsalt = MyHmac.GenerateSalt();
            user.SALT = Convert.ToBase64String(hsalt);
            var password = APIAuthFunctions.GeneratePassword();

            var hmac = MyHmac.GenerateSaltedHash(Encoding.UTF8.GetBytes(password),
                Encoding.UTF8.GetBytes(user.SALT));

            user.HASH = Convert.ToBase64String(hmac);

            TitanAuth.SaveChanges();

            string myemail = user.UsrEmailAddress;

            SmtpClient.Email(myemail, "Your account has been updated for the Titan App",
                            "<p>Hi" + user.UsrFirstName + ",</p><p> Please find your account " +
                            "details and shortcut to our Titan app.</p><p>" +
                            "<a href = 'http://titan/'> http://titan</a></p><p> User Name:" +
                            user.UsrName + " </p><p> Password:" + password + "</p><p > " +
                            "Please note that this is in a pre - release state and is expected " +
                            "to have bugs and further feature improvement.</ p >");

            //Security.Log(new SecurityLogDetails(HttpContext, $"{user.UsrName}'s password was " +
            //    $"reset by an administrator")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "ActorIdentity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user },
            //            { "ActorToken", HttpContext.GetTokenAsync("access_token").Result },
            //        }
            //});

            return Ok();
        }

        // POST api/<PasswordController>/InitiateReset
        /// <summary>
        /// Start a user password reset by sending a token to their email
        /// </summary>
        /// <param name="emailAddress">The email address of the user</param>
        /// <response code="200">Successful reset request</response>
        /// <response code="400">
        /// The request is invalid for any reason (e.g. invalid email address)
        /// </response>
        [HttpDelete("email/{emailAddress}")]
        public ActionResult StartPasswordReset(string emailAddress)
        {
            var user = TitanAuth.Users.FirstOrDefault(usr => usr.UsrEmailAddress == emailAddress);

            if (user == null)
            {
                return BadRequest();
            }

            user.UsrResetDateTime = DateTime.UtcNow;

            user.UsrResetToken = Guid.NewGuid().ToString();

            TitanAuth.SaveChanges();

            SmtpClient.Email(emailAddress, "Security Notification", "Please reset " +
                "your password using the following link, please note that this link will expire" +
                " in 15 minutes and a new reset request will have to be submited " +
                "http://titan/account/updatepassword?token=" + user.UsrResetToken);

            //Security.Log(new SecurityLogDetails(HttpContext, $"{user.UsrName} started to " +
            //    $"reset thier password")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user },
            //            { "Token", HttpContext.GetTokenAsync("access_token").Result },
            //            { "IP", HttpContext.Connection.RemoteIpAddress },
            //            { "ResetToken", user.UsrResetToken }
            //        }
            //});

            return Ok();
        }

        // POST api/<PasswordController>
        /// <summary>
        /// Change a users password using the token delivered in an email
        /// </summary>
        /// <param name="resetRequest">The token, and new password provided by the user</param>
        /// <response code="200">Successful password change by user</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid token)
        /// </response>
        [HttpPost]
        public ActionResult ResetPasswordUsingToken(TokenChangePasswordRequest resetRequest)
        {
            var user =
                TitanAuth.Users.FirstOrDefault(usr => usr.UsrResetToken == resetRequest.Token);

            if (user == null)
            {
                return BadRequest();
            }

            DateTime startDate = System.DateTime.UtcNow;
            DateTime? endDate = user.UsrResetDateTime;

            DateTime newdate = endDate.Value;

            TimeSpan hours = startDate - newdate;

            if (hours.TotalMinutes >= 15)
            {
                //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"{user.UsrName} tried to" +
                //    $" reset thier password but token timed out")
                //{
                //    AdditionalInfo = new Dictionary<string, object> {
                //        { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
                //        { "User", user },
                //        { "Token", HttpContext.GetTokenAsync("access_token").Result },
                //        { "IP", HttpContext.Connection.RemoteIpAddress },
                //        { "ResetToken", user.UsrResetToken }
                //    }
                //});

                return BadRequest();
            }

            // update password as requested
            var hsalt = MyHmac.GenerateSalt();
            user.SALT = Convert.ToBase64String(hsalt);

            var hmac = MyHmac.GenerateSaltedHash(Encoding.UTF8.GetBytes(resetRequest.Password),
                Encoding.UTF8.GetBytes(user.SALT));

            user.HASH = Convert.ToBase64String(hmac);

            TitanAuth.SaveChanges();

            string myemail = user.UsrEmailAddress;

            SmtpClient.Email(myemail, "Security Notification", "The password for your " +
                "Titan account " + myemail + " was changed. If you didn't change it, you " +
                "should contact IT Immediately");

            //Security.Log(new SecurityLogDetails(HttpContext, $"{user.UsrName} reset thier password")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user },
            //            { "Token", HttpContext.GetTokenAsync("access_token").Result },
            //            { "IP", HttpContext.Connection.RemoteIpAddress },
            //            { "ResetToken", user.UsrResetToken }
            //        }
            //});

            return Ok();
        }

        // PUT api/<PasswordController>
        /// <summary>
        /// Change a password of a user using thier password
        /// </summary>
        /// <param name="request">The username, current password and new password</param>
        /// <response code="200">Sucessfully changed users password</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid password)
        /// </response>
        [HttpPut]
        public ActionResult ChangePassword(ChangePasswordRequest request)
        {
            var user = TitanAuth.Users.FirstOrDefault(usr => usr.UsrName == request.Username);

            if (user == null)
            {
                return BadRequest();
            }

            if (user.UsrEnabled == false)
            {
                //Security.Log(new SecurityLogDetails(HttpContext, $"{user.UsrName} tries to " +
                //    $"change thier password while disabled")
                //{
                //    AdditionalInfo = new Dictionary<string, object> {
                //        { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
                //        { "User", user },
                //        { "Token", HttpContext.GetTokenAsync("access_token").Result },
                //        { "IP", HttpContext.Connection.RemoteIpAddress }
                //    }
                //});

                return BadRequest();
            }

            // verfiy current password
            var hmac = MyHmac.GenerateSaltedHash(Encoding.UTF8.GetBytes(request.Password),
                Encoding.UTF8.GetBytes(user.SALT));
            string myHASH = Convert.ToBase64String(hmac);

            if (user.HASH != myHASH)
            {
                //Security.Log(new SecurityLogDetails(HttpContext, $"{user.UsrName} tries to " +
                //    $"change thier password but fails due to incorrect password")
                //{
                //    AdditionalInfo = new Dictionary<string, object> {
                //        { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
                //        { "User", user },
                //        { "Token", HttpContext.GetTokenAsync("access_token").Result },
                //        { "IP", HttpContext.Connection.RemoteIpAddress }
                //    }
                //});

                return BadRequest(); // Username or Password do not match
            }

            // set new password
            var hsalt = MyHmac.GenerateSalt();
            user.SALT = Convert.ToBase64String(hsalt);

            hmac = MyHmac.GenerateSaltedHash(Encoding.UTF8.GetBytes(request.NewPassword),
                Encoding.UTF8.GetBytes(user.SALT));

            user.HASH = Convert.ToBase64String(hmac);

            TitanAuth.SaveChanges();

            string myemail = user.UsrEmailAddress;

            SmtpClient.Email(myemail, "Security Notification", "The password for your" +
                " Titan account " + myemail + " was changed. If you didn't change it, " +
                "you should contact IT Immediately");

            //Security.Log(new SecurityLogDetails(HttpContext, $"{user.UsrName} changes thier password")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user },
            //            { "Token", HttpContext.GetTokenAsync("access_token").Result },
            //            { "IP", HttpContext.Connection.RemoteIpAddress }
            //        }
            //});

            return Ok();

        }
    }
}
