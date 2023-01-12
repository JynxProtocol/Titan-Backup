using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Titan.Data;
using Titan.Filters;
using Titan.Models;
using Titan.Models.AccessControl;
using Titan.Models.AccessControl.Roles;
using Titan.Models.DBUser;


namespace Titan.API.Administration.Controllers
{
    /// <summary>
    /// Roles endpoints are used to manage user roles and the features they give access too
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RolesController : ControllerBase
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public RolesController(TitanAuthContext _db)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            TitanAuth = _db;
        }

        internal TitanAuthContext TitanAuth { get; }

        // GET: api/<RolesController>
        /// <summary>
        /// Get a list of roles, the users they contain and the features they can access
        /// </summary>
        /// <returns>A list of all roles</returns>
        /// <response code="200">Returns the list of roles</response>
        [HttpGet]
        [Feature("RolesSee")]
        public ActionResult<IEnumerable<ViewRole>> GetAllRoles()
        {
            IEnumerable<Role> TitanAuthRoles = TitanAuth.Roles.ToList();
            List<ViewRole> roles = new();

            foreach (Role role in TitanAuthRoles)
            {
                ViewRole ViewRole = new()
                {
                    ID = role.ID,
                    RoleName = role.RoleName,
                    Features = GetFeaturesByRole(role.ID).ToArray(),
                    Users = (from user in TitanAuth.Users
                             join roleMapping in TitanAuth.UserRoleMappings
                             on user.UsrID equals roleMapping.UsrID
                             where roleMapping.RoleID == role.ID
                             select user.UsrName).Distinct().ToList()
                };

                ViewRole.FeatureAreas = GetFeatureAreas(ViewRole).ToArray();

                roles.Add(ViewRole);
            }

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User gets a list of all roles")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Roles", roles }
            //    }
            //});

            return Ok(roles);
        }

        private Dictionary<Feature, bool> GetFeaturesByRole(int id)
        {
            // all features in database
            var allFeatures = TitanAuth.Features.ToList();

            var DBMappings = (from TitanAuthRole in TitanAuth.Roles
                              join featureMapping in TitanAuth.RoleFeatureMappings
                              on TitanAuthRole.ID equals featureMapping.RoleID
                              where TitanAuthRole.ID == id
                              select featureMapping.FeatureID).Distinct().ToList();

            Dictionary<Feature, bool> featureList = new();

            foreach (Feature feature in allFeatures)
            {
                featureList[feature] = DBMappings.Contains(feature.ID);
            }

            return featureList;
        }

        private Dictionary<string, Trinary> GetFeatureAreas(ViewRole role)
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

                if (role.Features.Any(Feature => ((Feature.Key.FeatureArea == featureArea) && Feature.Value)))
                {
                    roleFeatureAreas[featureArea] = Trinary.All;

                    if (role.Features.Any(Feature => ((Feature.Key.FeatureArea == featureArea) && !Feature.Value)))
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

        // GET api/<RolesController>/5
        /// <summary>
        /// Get a role by id, the users it contains and the features it can access
        /// </summary>
        /// <param name="id">The id of the role to return</param>
        /// <returns>The role with the given id</returns>
        /// <response code="200">Returns the role</response>
        /// <response code ="404">The role is not found</response>
        [HttpGet("{id}")]
        [Feature("RolesSee")]
        public ActionResult<ViewRole> GetRoleById(int id)
        {
            Role DBRole = TitanAuth.Roles.FirstOrDefault(Role => Role.ID == id);

            if (DBRole == null)
            {
                return NotFound();
            }

            ViewRole role = new()
            {
                ID = DBRole.ID,
                RoleName = DBRole.RoleName,
                Features = GetFeaturesByRole(DBRole.ID).ToArray(),
                Users = (from user in TitanAuth.Users
                         join roleMapping in TitanAuth.UserRoleMappings
                         on user.UsrID equals roleMapping.UsrID
                         where roleMapping.RoleID == DBRole.ID
                         select user.UsrName).Distinct().ToList()
            };

            role.FeatureAreas = GetFeatureAreas(role).ToArray();

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User gets details of role {role.RoleName}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Role", role }
            //    }
            //});

            return Ok(role);
        }

        // GET api/<RolesController>/{id}/Users
        /// <summary>
        /// Get a list of all users and whether they are in the given role
        /// </summary>
        /// <param name="id">The role id to check membership for</param>
        /// <returns>A list of all users and whether they are in the given role</returns>
        /// <response code="200">The list of users and whether they are in the role</response>
        [HttpGet("{id}/Users")]
        [Feature("UsersSee")]
        public ActionResult<ViewRoleUsers> RoleUsers(int id)
        {
            // all users in database
            var allUsers = (from user in TitanAuth.Users
                            where user.UsrName != null
                            select user.UsrName).Distinct().ToList();

            var DBUsers = (from roleMapping in TitanAuth.UserRoleMappings
                           join user in TitanAuth.Users
                           on roleMapping.UsrID equals user.UsrID
                           where roleMapping.RoleID == id & user.UsrName != null
                           select user.UsrName).Distinct().ToList();

            Dictionary<string, bool> userList = new();

            foreach (string user in allUsers)
            {
                userList[user] = DBUsers.Contains(user);
            }


            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User gets users in role id {id}"));

            return Ok(new ViewRoleUsers { Users = userList });
        }

        // PUT api/<RolesController>/{id}/Users
        /// <summary>
        /// Change which users are in a given role
        /// </summary>
        /// <param name="id">The role to update membership for</param>
        /// <param name="editRoleUsers">A list of users and whether they are in the role</param>
        /// <returns>The updated list of users and role membership</returns>
        /// <response code="200">Succesfully updated a roles membership</response>
        /// <response code="403">Admin permissions were modified incorrectly</response>
        /// <response code="422">A user membership record refers to an invalid user</response>
        [HttpPut("{id}/Users")]
        [Feature("RolesManage")]
        public ActionResult<ViewRoleUsers> EditRoleUsers(int id, EditRoleUsers editRoleUsers)
        {
            // this snippet is part of the protections stopping someone from accidentally
            // breaking the admin account's access and permissions
            //  - The Admin role should have access to all permissions, or at the very least the
            // ones it needs to manage users
            //  - The Admin user should always be in the admin role
            //  - Neither should be deletable
            // IMPROVEMENT: make this more centralised, easier to understand, hard-code
            // that everyone is in the everyone role, that the admin role has certain permissions
            if (editRoleUsers.Users.Any(User => User.Key == "Admin" & !User.Value) ||
                id == TitanAuth.Roles.Single(role => role.RoleName == "Everyone").ID)
            {
                return Forbid();
            }

            var DBMappings = (from userMapping in TitanAuth.UserRoleMappings
                              where userMapping.RoleID == id
                              select userMapping).Distinct().ToList();

            // remove URMs when editing, otherwise could cause problems with data persist
            foreach (UserRoleMapping userRoleMapping in DBMappings)
            {
                TitanAuth.UserRoleMappings.Remove(userRoleMapping);
            }

            foreach (KeyValuePair<string, bool> UsersKVP in editRoleUsers.Users)
            {
                DBUser user = TitanAuth.Users.FirstOrDefault(User => User.UsrName == UsersKVP.Key);

                if (user == null)
                {
                    return new UnprocessableEntityResult();
                }

                if (UsersKVP.Value)
                {
                    TitanAuth.UserRoleMappings.Add(new UserRoleMapping
                    {
                        UsrID = user.UsrID,
                        RoleID = id
                    });
                }
            }
            TitanAuth.SaveChanges();

            // all users in database
            var allUsers = (from user in TitanAuth.Users
                            where user.UsrName != null
                            select user.UsrName).Distinct().ToList();

            var DBUsers = (from roleMapping in TitanAuth.UserRoleMappings
                           join user in TitanAuth.Users
                           on roleMapping.UsrID equals user.UsrID
                           where roleMapping.RoleID == id & user.UsrName != null
                           select user.UsrName).Distinct().ToList();

            Dictionary<string, bool> userList = new();

            foreach (string user in allUsers)
            {
                userList[user] = DBUsers.Contains(user);
            }

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"User edits users in role id {id}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "Users", userList }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User edits users in role id {id}")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Users", userList }
            //    }
            //});

            return Ok(new ViewRoleUsers { Users = userList });
        }

        // GET api/<RolesController>/5/features
        /// <summary>
        /// Get the features a given role has access to
        /// </summary>
        /// <param name="id">The id of the role to return the features of </param>
        /// <returns>The list of features a role gives access to</returns>
        /// <response code="200">A list of the roles features</response>
        /// <response code ="404">The role is not found</response>
        [HttpGet("{id}/Features")]
        [Feature("FeaturesSee")]
        public ActionResult<IEnumerable<Feature>> RoleFeatures(int id)
        {
            Role role = TitanAuth.Roles.FirstOrDefault(Role => Role.ID == id);

            if (role == null)
            {
                return NotFound();
            }

            var features = (from feature in TitanAuth.Features
                            join featureMapping in TitanAuth.RoleFeatureMappings
                            on feature.ID equals featureMapping.FeatureID
                            where featureMapping.RoleID == role.ID
                            select feature).ToList();

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"User views features of role id {id}"));

            return Ok(features);
        }

        // POST api/<RolesController>
        /// <summary>
        /// Create a new role with a given name
        /// </summary>
        /// <param name="role">The role to create</param>
        /// <returns>The newly created role</returns>
        /// <response code="200">The succesfully created role</response>
        /// <response code="400">The request is invalid</response>
        [HttpPost]
        [Feature("RolesManage")]
        public ActionResult<ViewRole> CreateRole(CreateRole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Role newRole = new();
            newRole.RoleName = role.RoleName;

            TitanAuth.Roles.Add(newRole);
            TitanAuth.SaveChanges();

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"New role {role.RoleName} created")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "Role", newRole }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"New role {role.RoleName} created")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Role", newRole }
            //    }
            //});

            return Ok(newRole);
        }

        // PUT api/<RolesController>/5
        /// <summary>
        /// Edit a roles' features
        /// </summary>
        /// <param name="id">The role id to edit</param>
        /// <param name="editRole">
        /// The updated list of features and whether the role can access them
        /// </param>
        /// <returns>The updated role</returns>
        /// <response code="200">The succesfully edited role</response>
        /// <response code="403">Admin permissions were modified incorrectly</response>
        /// <response code="404">The role was not found</response>
        /// <response code="422">A feature access record refers to an invalid feature</response>
        [HttpPut("{id}")]
        [Feature("RolesManage")]
        public ActionResult<ViewRole> EditRole(int id, EditRole editRole)
        {
            Role role = TitanAuth.Roles.FirstOrDefault(Role => Role.ID == id);

            if (role == null)
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
            if (role.RoleName == "Admin")
            {
                return Forbid();
            }

            // remove current features
            var DBMappings = (from TitanAuthRole in TitanAuth.Roles
                              join featureMapping in TitanAuth.RoleFeatureMappings
                              on TitanAuthRole.ID equals featureMapping.RoleID
                              where TitanAuthRole.ID == role.ID
                              select featureMapping).ToList();

            foreach (RoleFeatureMapping roleFeatureMapping in DBMappings)
            {
                TitanAuth.RoleFeatureMappings.Remove(roleFeatureMapping);
            }

            // add new features
            // editing in-place would be errorprone
            foreach (KeyValuePair<string, bool> KVP in editRole._Features)
            {
                if (KVP.Value)
                {
                    var feature = TitanAuth.Features.FirstOrDefault(Feature => Feature.FeatureName == KVP.Key);
                    if (feature == null)
                    {
                        return new UnprocessableEntityResult();
                    }

                    RoleFeatureMapping roleFeatureMapping = new();
                    roleFeatureMapping.FeatureID = feature.ID;
                    roleFeatureMapping.RoleID = role.ID;
                    TitanAuth.RoleFeatureMappings.Add(roleFeatureMapping);
                }
            }

            TitanAuth.SaveChanges();

            ViewRole viewRole = new()
            {
                ID = role.ID,
                RoleName = role.RoleName,
                Features = GetFeaturesByRole(role.ID).ToArray(),
                Users = (from user in TitanAuth.Users
                         join roleMapping in TitanAuth.UserRoleMappings
                         on user.UsrID equals roleMapping.UsrID
                         where roleMapping.RoleID == role.ID
                         select user.UsrName).Distinct().ToList()
            };

            viewRole.FeatureAreas = GetFeatureAreas(viewRole).ToArray();

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"Role {role.RoleName} edited")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "Role", viewRole }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"Role {role.RoleName} edited")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Role", viewRole }
            //    }
            //});

            return Ok(viewRole);
        }

        // DELETE api/<RolesController>/5
        /// <summary>
        /// Delete a given role
        /// </summary>
        /// <param name="id">The role to delete</param>
        /// <response code="200">Successfuly deleted role</response>
        /// <response code="403">Admin permissions were modified incorrectly</response>
        /// <response code="404">The role is not found</response>
        [HttpDelete("{id}")]
        [Feature("RolesManage")]
        public ActionResult DeleteRole(int id)
        {
            Role remove = TitanAuth.Roles.FirstOrDefault(Role => Role.ID == id);

            if (remove == null)
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
            if (remove.RoleName == "Admin")
            {
                return Forbid();
            }

            TitanAuth.Roles.Remove(remove);

            var DBMappings = (from TitanAuthRole in TitanAuth.Roles
                              join featureMapping in TitanAuth.RoleFeatureMappings
                              on TitanAuthRole.ID equals featureMapping.RoleID
                              where TitanAuthRole.ID == remove.ID
                              select featureMapping).ToList();

            // remove RFMs when removing role, otherwise could cause problems with roleid auto-increment
            foreach (RoleFeatureMapping roleFeatureMapping in DBMappings)
            {
                TitanAuth.RoleFeatureMappings.Remove(roleFeatureMapping);
            }

            //Security.LogVerbose(new SecurityLogDetails(HttpContext, $"Role {remove.RoleName} deleted")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Identity", (ClaimsIdentity)HttpContext.User?.Identity },
            //            { "Role", remove }
            //    }
            //});

            //Generic.LogVerbose(new GenericLogDetails(HttpContext, $"Role {remove.RoleName} deleted")
            //{
            //    AdditionalInfo = new Dictionary<string, object> {
            //            { "Role", remove }
            //    }
            //});

            TitanAuth.SaveChanges();
            return Ok();
        }
    }
}
