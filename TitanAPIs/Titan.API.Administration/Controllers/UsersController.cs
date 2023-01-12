using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Titan.Data;
using Titan.Filters;
using Titan.Functions;
using Titan.Models;
using Titan.Models.AccessControl;
using Titan.Models.AccessControl.Roles;
using Titan.Models.DBUser;
using Titan.Models.User;
using Titan.Models.UserConversions;

namespace Titan.API.Administration.Controllers
{
    /// <summary>
    /// Users endpoints are used to manage users, thier roles and what they can access
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public UsersController(IConfiguration configuration, TitanAuthContext _db,
            SmtpClient smtpClient)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            TitanAuth = _db;
            Configuration = configuration;
            SmtpClient = smtpClient;
        }

        internal IConfiguration Configuration { get; private set; }
        internal TitanAuthContext TitanAuth { get; private set; }
        internal SmtpClient SmtpClient { get; private set; }

        /// <summary>Function <c>NewFromCreateUser</c> returns a new DBUser, adding it to the 
        /// database and emailing, with properties from the given CreateUser input</summary>
        internal DBUser NewFromCreateUser(CreateUser CreateUser)
        {
            var DBUser = new DBUser
            {
                UsrName = CreateUser.UsrName,
                UsrFirstName = CreateUser.UsrFirstName,
                UsrLastName = CreateUser.UsrLastName,
                UsrEmailAddress = CreateUser.UsrEmailAddress,
                UsrDepartment = CreateUser.UsrDepartment,
                UsrEnabled = CreateUser.UsrEnabled,

                ShowInLookUp = 1
            };

            DBUser.UsrDisplayName = DBUser.UsrFirstName + ' ' + DBUser.UsrLastName;
            DBUser.UsrUUID = System.Guid.NewGuid();

            DBUser.SALT = Convert.ToBase64String(MyHmac.GenerateSalt());

            string password = APIAuthFunctions.GeneratePassword();

            DBUser.HASH = Convert.ToBase64String(MyHmac.GenerateSaltedHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(DBUser.SALT)));

            DBUser.UsrLoginCount = 0;

            TitanAuth.Add(DBUser);

            // add user to everyone role
            TitanAuth.UserRoleMappings.Add(new UserRoleMapping
            {
                RoleID = TitanAuth.Roles.Single(role => role.RoleName == "Everyone").ID,
                UsrID = DBUser.UsrID
            });

            TitanAuth.SaveChanges();

            SmtpClient.Email(DBUser.UsrEmailAddress, "You have been added to Titan",
                    $"<p>Hi {DBUser.UsrFirstName}, </p><p> Please find your account details and shortcut to our Titan app.</p>" +
                    "<p><a href = 'http://titan'> http://titan</a></p>" +
                    "<p> User Name:" + DBUser.UsrName + " </p>" +
                    "<p> Password:" + password + "</p>" +
                    "<p> Please note that this is in a pre - release state and is expected to have bugs and further feature improvement.</ p >" +
                    "<p> Please can you record all issues and feature requests clearly identifying this to be a bug or feature and send them to darren.pratt@sabrerail.com.</p>");

            return DBUser;
        }

        // GET: api/<UsersController>
        /// <summary>
        /// Get a list of all users
        /// </summary>
        /// <returns>A list of all users</returns>
        /// <response code="200">Returns the list fo users</response>
        [HttpGet]
        [Feature("UsersSee")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            List<DBUser> users = TitanAuth.Users.ToList();
            List<User> santitizedUsers = new();

            foreach (DBUser user in users)
            {
                User newClientUser = user.ToClientUser();
                santitizedUsers.Add(newClientUser);
            }

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"GDPR: A user gets a list of all users")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"A user gets a list of all users"));

            return Ok(santitizedUsers);
        }

        // GET api/<UsersController>/5
        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id">The id of the user to return</param>
        /// <returns>The user with the given id</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">The user was not found</response>
        [HttpGet("{id:int}")]
        [Feature("UsersSee")]
        public ActionResult<User> GetUserById(int id)
        {
            DBUser user = TitanAuth.Users.FirstOrDefault(User => User.UsrID == id);

            if (user == null)
            {
                return NotFound();
            }

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"GDPR: A user gets a user {user.UsrName}'s details")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "Details", user.ToClientUser() }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"A user gets a user {user.UsrName}'s details")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Details", user.ToClientUser() }
            //    }
            //});

            return base.Ok((object)user.ToClientUser());
        }

        // GET api/<UsersController>/5
        /// <summary>
        /// Get the currently logged in user
        /// </summary>
        /// <returns>The currently logged in users details</returns>
        /// <response code="200">Returns the logged in user</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="404">The user was not found</response>
        [HttpGet("current")]
        public ActionResult<User> GetCurrentUser()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return new UnauthorizedResult();
            }

            string UsrID = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // get authorised user from TitanAuth
            DBUser user = TitanAuth.Users.FirstOrDefault(User => User.UsrID.ToString() == UsrID);

            if (user == null)
            {
                return NotFound();
            }

            if (user.UsrEnabled == false)
            {
                return Forbid();
            }

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User {user.UsrName} gets thier own details")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Details", user.ToClientUser() }
            //    }
            //});

            return base.Ok((object)user.ToClientUser());
        }

        // GET api/<UsersController>/{id}/Roles
        /// <summary>
        /// Get a list of all roles and whether the user is a member
        /// </summary>
        /// <param name="_id">The id of the user to list roles for</param>
        /// <returns>A list of roles and user membership</returns>
        /// <response code="200">Returns the list of user role membership</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid user id)
        /// </response>
        /// <response code="404">The user was not found</response>
        [HttpGet("{_id}/Roles")]
        [Feature("RolesSee")]
        public ActionResult<ViewUserRoles> GetUserRoles(string _id)
        {
            // parse user id (get current if applicable)
            int id;
            if (_id == "current")
            {
                var userClaim = HttpContext.User.Claims.FirstOrDefault(Claim => Claim.Type == ClaimTypes.NameIdentifier);

                if (userClaim == null)
                {
                    return NotFound();
                }

                if (!int.TryParse(userClaim.Value, out id))
                {
                    return BadRequest();
                }
            }
            else
            {
                if (!int.TryParse(_id, out id))
                {
                    return BadRequest();
                }
            }

            // all roles in database
            var allRoles = (from role in TitanAuth.Roles
                            select role.RoleName).Distinct().ToList();

            var DBRoles = (from roleMapping in TitanAuth.UserRoleMappings
                           join role in TitanAuth.Roles
                           on roleMapping.RoleID equals role.ID
                           where roleMapping.UsrID == id
                           select role.RoleName).Distinct().ToList();

            Dictionary<string, bool> roleList = new();

            foreach (string role in allRoles)
            {
                roleList[role] = DBRoles.Contains(role);
            }

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"GDPR: A user gets the roles of UserId {id}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "Id", id }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"A user gets the roles of UserId {id}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Id", id }
            //    }
            //});

            return Ok(new ViewUserRoles { Roles = roleList });
        }

        // PUT api/<UsersController>/{id}/Roles
        /// <summary>
        /// Edit the roles a user is a member of
        /// </summary>
        /// <param name="_id">The user to edit membership for</param>
        /// <param name="editUserRoles">The updated membership of roles</param>
        /// <returns>The succesfully updated role membership values</returns>
        /// <response code="200">Returns the updated membership values</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid user id)
        /// </response>
        /// <response code="404">The user was not found</response>
        /// <response code="422">A role membership record refers to an invalid role</response>
        [HttpPut("{_id}/Roles")]
        [Feature("RolesManage")]
        public ActionResult<ViewUserRoles> EditUserRoles(string _id, EditUserRoles editUserRoles)
        {
            // parse user id (get current if applicable)
            int id;
            if (_id == "current")
            {
                var userClaim = HttpContext.User.Claims.FirstOrDefault(Claim => Claim.Type == ClaimTypes.NameIdentifier);

                if (userClaim == null)
                {
                    return NotFound();
                }

                if (!int.TryParse(userClaim.Value, out id))
                {
                    return BadRequest();
                }
            }
            else
            {
                if (!int.TryParse(_id, out id))
                {
                    return BadRequest();
                }
            }

            var DBMappings = (from userMapping in TitanAuth.UserRoleMappings
                              where userMapping.UsrID == id
                              select userMapping).Distinct().ToList();

            // remove URMs when editing, otherwise could cause problems with data persist
            foreach (UserRoleMapping userRoleMapping in DBMappings)
            {
                // skip removing from everyone role
                if (userRoleMapping.RoleID == TitanAuth.Roles
                    .Single(role => role.RoleName == "Everyone").ID)
                {
                    continue;
                }

                TitanAuth.UserRoleMappings.Remove(userRoleMapping);
            }

            foreach (KeyValuePair<string, bool> RolesKVP in editUserRoles.Roles)
            {
                Role role = TitanAuth.Roles.FirstOrDefault(Role => Role.RoleName == RolesKVP.Key);

                if (role == null)
                {
                    return new UnprocessableEntityResult();
                }

                if (RolesKVP.Value)
                {
                    // skip adding to everyone role
                    if (role.ID == TitanAuth.Roles
                        .Single(role => role.RoleName == "Everyone").ID)
                    {
                        continue;
                    }

                    TitanAuth.UserRoleMappings.Add(new UserRoleMapping
                    {
                        RoleID = role.ID,
                        UsrID = id
                    });
                }
            }
            TitanAuth.SaveChanges();

            // all roles in database
            var allRoles = (from role in TitanAuth.Roles
                            select role.RoleName).Distinct().ToList();

            var DBRoles = (from roleMapping in TitanAuth.UserRoleMappings
                           join role in TitanAuth.Roles
                           on roleMapping.RoleID equals role.ID
                           where roleMapping.UsrID == id
                           select role.RoleName).Distinct().ToList();

            Dictionary<string, bool> roleList = new();

            foreach (string role in allRoles)
            {
                roleList[role] = DBRoles.Contains(role);
            }

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"A user changes the roles of UserId {id}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "NewRoles", DBRoles }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"A user edits the roles of UserId {id}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "NewRoles", DBRoles }
            //    }
            //});

            return Ok(new ViewUserRoles { Roles = roleList });
        }

        private Dictionary<Feature, bool> GetFeaturesByUser(int id)
        {
            // all features in database
            var allFeatures = TitanAuth.Features.ToList();

            var DBMappings = (from TitanAuthRole in TitanAuth.Roles
                              join featureMapping in TitanAuth.RoleFeatureMappings
                              on TitanAuthRole.ID equals featureMapping.RoleID
                              join userMapping in TitanAuth.UserRoleMappings
                              on TitanAuthRole.ID equals userMapping.RoleID
                              where userMapping.UsrID == id
                              select (int)featureMapping.FeatureID).Distinct().ToList();

            Dictionary<Feature, bool> featureList = new();

            foreach (Feature feature in allFeatures)
            {
                featureList[feature] = DBMappings.Contains(feature.ID);
            }

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"A user gets the features UserId {id} has access to")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Id", id },
            //            { "Features", featureList }
            //    }
            //});

            return featureList;
        }

        private Dictionary<string, Trinary> GetFeatureAreas(
            KeyValuePair<Feature, bool>[] featureArray)
        {
            var allFeatureAreas = (from feature in TitanAuth.Features
                                   select feature.FeatureArea).Distinct().ToList();

            Dictionary<string, Trinary> roleFeatureAreas = new();

            foreach (string featureArea in allFeatureAreas)
            {
                if (!roleFeatureAreas.ContainsKey(featureArea))
                {
                    roleFeatureAreas[featureArea] = Trinary.None;
                }
            }

            foreach (string featureArea in roleFeatureAreas.Keys)
            {
                roleFeatureAreas[featureArea] = Trinary.All;

                if (featureArray.Any(Feature => ((Feature.Key.FeatureArea == featureArea) && Feature.Value)))
                {
                    roleFeatureAreas[featureArea] = Trinary.All;

                    if (featureArray.Any(Feature => ((Feature.Key.FeatureArea == featureArea) && !Feature.Value)))
                    {
                        roleFeatureAreas[featureArea] = Trinary.Some;
                    }
                }
                else
                {
                    roleFeatureAreas[featureArea] = Trinary.None;
                }
            }

            return roleFeatureAreas;
        }

        // GET api/<UsersController>/{id}/permissions
        /// <summary>
        /// Get a list of features a user can access due to thier roles
        /// </summary>
        /// <param name="_id">The id of the user to list access for</param>
        /// <returns>A list of features and areas a user can access</returns>
        /// <response code="200">Returns the features a user can access</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid user id)
        /// </response>
        /// <response code="404">The user was not found</response>
        [HttpGet("{_id}/EffectiveAccess")]
        [Feature()]
        public ActionResult<UserEffectiveAccess> GetUserEffectiveAccess(string _id)
        {
            // parse user id (get current if applicable)
            int id;
            if (_id == "current")
            {
                var userClaim = HttpContext.User.Claims.FirstOrDefault(Claim => Claim.Type == ClaimTypes.NameIdentifier);

                if (userClaim == null)
                {
                    return NotFound();
                }

                if (!int.TryParse(userClaim.Value, out id))
                {
                    return BadRequest();
                }
            }
            else
            {
                if (!int.TryParse(_id, out id))
                {
                    return BadRequest();
                }
            }

            DBUser user = TitanAuth.Users.FirstOrDefault(User => User.UsrID == id);

            if (user == null)
            {
                return NotFound();
            }

            UserEffectiveAccess access = new()
            {
                Features = GetFeaturesByUser(id).ToArray()
            };
            access.FeatureAreas = GetFeatureAreas(access.Features).ToArray();

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"A user gets the effective access of UserId {id}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "Access", access }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"A user gets the effective access of UserId {id}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Access", access }
            //    }
            //});

            return Ok(access);
        }

        // GET api/<UsersController>/{id}/permission/{permission}
        /// <summary>
        /// Check if a user has a permission given by thier roles
        /// </summary>
        /// <param name="_id">The id of the user to check permission for</param>
        /// <param name="permission">The permission to check</param>
        /// <returns>Whether the user has the given permission</returns>
        /// <response code="200">Returns whether the user has the premission</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid user id)
        /// </response>
        /// <response code="404">The user was not found</response>
        [HttpGet("{_id}/Permission/{permission}")]
        public ActionResult<bool> UserHasPerm(string _id, string permission)
        {
            // parse user id (get current if applicable)
            int id;
            if (_id == "current")
            {
                var userClaim = HttpContext.User.Claims.FirstOrDefault(Claim => Claim.Type == ClaimTypes.NameIdentifier);

                if (userClaim == null)
                {
                    return NotFound();
                }

                if (!int.TryParse(userClaim.Value, out id))
                {
                    return BadRequest();
                }
            }
            else
            {
                if (!int.TryParse(_id, out id))
                {
                    return BadRequest();
                }
            }

            DBUser user = TitanAuth.Users.FirstOrDefault(User => User.UsrID == id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.UsrEnabled == false)
            {
                return BadRequest();
            }

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

            // This function is ran CONSTANTLY, maybe don't log this one

            return Ok(userFeatures.Contains(permission));
        }

        // POST api/<UsersController>
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">The details of the user to create</param>
        /// <returns>The created user</returns>
        /// <response code="200">Succesfully created user</response>
        /// <response code="400">The request is invalid</response>
        [HttpPost]
        [Feature("UsersManage")]
        public ActionResult<User> CreateUser(CreateUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            DBUser newDBUser = NewFromCreateUser(user);

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"A new user {newDBUser.UsrName} was created")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "NewUser", user }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"A new user {newDBUser.UsrName} was created")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Access", user }
            //    }
            //});

            return Ok(user);
        }

        // PUT api/<UsersController>/5/Enabled
        /// <summary>
        /// Set whether a user is enabled
        /// </summary>
        /// <param name="_id">The id of the user to update</param>
        /// <param name="enabled">Whether the user is enabled</param>
        /// <response code="200">Succesfully updated users enabled status</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid user id)
        /// </response>
        /// <response code="403">Admin permissions were modified incorrectly</response>
        /// <response code="404">The user was not found</response>
        [HttpPut("{_id}/Enabled")]
        [Feature("UsersManage")]
        public ActionResult UserSetEnabled(string _id, [FromBody] bool enabled)
        {
            // parse user id (get current if applicable)
            int id;
            if (_id == "current")
            {
                var userClaim = HttpContext.User.Claims.FirstOrDefault(Claim => Claim.Type == ClaimTypes.NameIdentifier);

                if (userClaim == null)
                {
                    return NotFound();
                }

                if (!int.TryParse(userClaim.Value, out id))
                {
                    return BadRequest();
                }
            }
            else
            {
                if (!int.TryParse(_id, out id))
                {
                    return BadRequest();
                }
            }

            var user = TitanAuth.Users.FirstOrDefault(User => User.UsrID == id);

            if (user == null)
            {
                return NotFound();
            }

            // this snippet is part of the protections stopping someone from accidentally
            // breaking the admin account's access and permissions
            //  - The Admin role should have access to all permissions, or at the very least the
            // ones it needs to manage users
            //  - The Admin user should always be in the admin role
            //  - Neither should be deletable
            // IMPROVEMENT: make this more centralised, easier to understand, hard-code
            // that everyone is in the everyone role, that the admin role has certain permissions
            if (user.UsrName == "Admin")
            {
                return Forbid();
            }

            user.UsrEnabled = enabled;

            TitanAuth.SaveChanges();

            //Security.Log(new SecurityLogDetails(HttpContext, $"User {user.UsrName} is {(enabled ? "enabled" : "disabled")}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user.ToClientUser() }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User {user.UsrName} is {(enabled ? "enabled" : "disabled")}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Access", user }
            //    }
            //});

            return Ok();
        }

        // PUT api/<UsersController>/5
        /// <summary>
        /// Edit an existing user 
        /// </summary>
        /// <param name="_id">The user to edit</param>
        /// <param name="user">The updated user information</param>
        /// <returns>The updated user</returns>
        /// <response code="200">Succesfully updated user</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid user id)
        /// </response>
        /// <response code="403">Admin permissions were modified incorrectly</response>
        /// <response code="404">The user was not found</response>
        [HttpPut("{_id}")]
        [Feature("UsersManage")]
        public ActionResult<EditUser> EditUser(string _id, EditUser user)
        {
            // parse user id (get current if applicable)
            int id;
            if (_id == "current")
            {
                var userClaim = HttpContext.User.Claims.FirstOrDefault(Claim => Claim.Type == ClaimTypes.NameIdentifier);

                if (userClaim == null)
                {
                    return NotFound();
                }

                if (!int.TryParse(userClaim.Value, out id))
                {
                    return BadRequest();
                }
            }
            else
            {
                if (!int.TryParse(_id, out id))
                {
                    return BadRequest();
                }
            }

            if (ModelState.IsValid)
            {
                DBUser DBUser = TitanAuth.Users.FirstOrDefault(User => User.UsrID == id);

                if (DBUser == null)
                {
                    return NotFound();
                }

                // this snippet is part of the protections stopping someone from accidentally
                // breaking the admin account's access and permissions
                //  - The Admin role should have access to all permissions, or at the very least the
                // ones it needs to manage users
                //  - The Admin user should always be in the admin role
                //  - Neither should be deletable
                // IMPROVEMENT: make this more centralised, easier to understand, hard-code
                // that everyone is in the everyone role, that the admin role has certain permissions
                if (DBUser.UsrName == "Admin")
                {
                    return Forbid();
                }

                DBUser.EditDBUser(user);
                TitanAuth.SaveChanges();

                //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"User {user.UsrName} is edited")
                //{
                //    AdditionalInfo = new Dictionary<string, object> {
                //        { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
                //        { "User", DBUser.ToClientUser() }
                //}
                //});

                //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User {user.UsrName} is edited")
                //{
                //    AdditionalInfo = new Dictionary<string, object> {
                //        { "User", DBUser.ToClientUser() }
                //}
                //});

                return base.Ok((object)DBUser.ToClientUser());
            }

            return BadRequest();
        }

        // DELETE api/<UsersController>/5
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">The user to delete</param>
        /// <response code="200">Succesfully deleted user</response>
        /// <response code="400">
        /// The request is invalid (e.g. if the user is not disabled)
        /// </response>
        /// <response code="403">Admin permissions were modified incorrectly</response>
        /// <response code="404">The user was not found</response>
        [HttpDelete("{id}")]
        [Feature("UsersManage")]
        public ActionResult<User> DeleteUser(int id)
        {
            DBUser user = TitanAuth.Users.FirstOrDefault(User => User.UsrID == id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.UsrEnabled)
            {
                return BadRequest();
            }

            // this snippet is part of the protections stopping someone from accidentally
            // breaking the admin account's access and permissions
            //  - The Admin role should have access to all permissions, or at the very least the
            // ones it needs to manage users
            //  - The Admin user should always be in the admin role
            //  - Neither should be deletable
            // IMPROVEMENT: make this more centralised, easier to understand, hard-code
            // that everyone is in the everyone role, that the admin role has certain permissions
            if (user.UsrName == "Admin")
            {
                return Forbid();
            }

            TitanAuth.Users.Remove(user);

            TitanAuth.SaveChanges();

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"User {user.UsrName} is deleted")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user.ToClientUser() }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User {user.UsrName} is deleted")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "User", user.ToClientUser() }
            //    }
            //});

            return Ok();
        }

        // PUT api/<UsersController>/5/Enabled
        /// <summary>
        /// Give barcode pin to a user
        /// </summary>
        /// <param name="_id">The user to generate a pin for</param>
        /// <response code="200">Succesfully given user a barcode pin</response>
        /// <response code="400">
        /// The request is invalid (e.g. invalid user id)
        /// </response>
        /// <response code="403">Admin permissions were modified incorrectly</response>
        /// <response code="404">The user was not found</response>
        /// <response code="409">The user already has a barcode pin</response>
        [HttpPut("{_id}/BarcodePin")]
        [Feature("UsersManage")]
        public ActionResult GiveUserBarcodePin(string _id)
        {
            // parse user id (get current if applicable)
            int id;
            if (_id == "current")
            {
                var userClaim = HttpContext.User.Claims.FirstOrDefault(Claim => Claim.Type == ClaimTypes.NameIdentifier);

                if (userClaim == null)
                {
                    return NotFound();
                }

                if (!int.TryParse(userClaim.Value, out id))
                {
                    return BadRequest();
                }
            }
            else
            {
                if (!int.TryParse(_id, out id))
                {
                    return BadRequest();
                }
            }

            var user = TitanAuth.Users.FirstOrDefault(User => User.UsrID == id);

            if (user == null)
            {
                return NotFound();
            }

            // this snippet is part of the protections stopping someone from accidentally
            // breaking the admin account's access and permissions
            //  - The Admin role should have access to all permissions, or at the very least the
            // ones it needs to manage users
            //  - The Admin user should always be in the admin role
            //  - Neither should be deletable
            // IMPROVEMENT: make this more centralised, easier to understand, hard-code
            // that everyone is in the everyone role, that the admin role has certain permissions
            if (user.UsrName == "Admin")
            {
                return Forbid();
            }

            if (!string.IsNullOrEmpty(user.UsrPin))
            {
                return Conflict();
            }

            // generate a new pin that is not in use
            user.UsrPin = APIAuthFunctions.GeneratePin(TitanAuth.Users.Select(User => User.UsrPin));

            TitanAuth.SaveChanges();

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"User {user.UsrName} is given a barcoding pin")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "User", user.ToClientUser() }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User {user.UsrName} is given a barcoding pin")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Access", user.ToClientUser() }
            //    }
            //});

            SmtpClient.Email(user.UsrEmailAddress, "You have been added to the stock barcoding system",
                    $"<p>Hi {user.UsrFirstName}, </p><p> Please find your pin code below</p><p> Pin code: {user.UsrPin} </p>");

            return Ok();
        }
    }
}
