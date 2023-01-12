using System;
using System.Linq;
using System.Web.Security;
using Titan.Data;

namespace Titan.Functions
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using AuthEntities _Context = new();
            var userRoles = (from user in _Context.Users
                             join roleMapping in _Context.UserRoleMappings
                             on user.UsrID equals roleMapping.UsrID
                             join role in _Context.Roles
                             on roleMapping.RoleID equals role.ID
                             where user.UsrName == username
                             select role.RoleName).ToArray();
            return userRoles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (AuthEntities _Context = new())
            {
                var userRoles = (from user in _Context.Users
                                 join roleMapping in _Context.UserRoleMappings
                                 on user.UsrID equals roleMapping.UsrID
                                 join role in _Context.Roles
                                 on roleMapping.RoleID equals role.ID
                                 where user.UsrName == username && role.RoleName == roleName
                                 select role.RoleName).ToList();

                if (userRoles.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}