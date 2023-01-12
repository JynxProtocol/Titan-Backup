using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titan.Models.User;
using Titan.Models.DBUser;

namespace Titan.Models.UserConversions
{
    // transport class representing the client representation of Users
    public static class UserConversions
    {
        /// <summary>Function <c>ToClientUser</c> returns a new User with the same properties as the given DBUser input</summary>
        public static User.User ToClientUser(this DBUser.DBUser DBUser)
        {
            var User = new User.User
            {
                UsrID = DBUser.UsrID,
                UsrName = DBUser.UsrName,
                UsrUUID = DBUser.UsrUUID,
                UsrFirstName = DBUser.UsrFirstName,
                UsrLastName = DBUser.UsrLastName,
                UsrEmailAddress = DBUser.UsrEmailAddress,
                UsrLastLogin = DBUser.UsrLastLogin,
                UsrLoginCount = DBUser.UsrLoginCount,
                UsrDisplayName = DBUser.UsrDisplayName,
                UsrDepartment = DBUser.UsrDepartment,
                UsrEnabled = DBUser.UsrEnabled
            };

            return User;
        }

        /// <summary>Function <c>EditDBUser</c> edits the current DBUser to have the properties specified by the given EditUser</summary>
        public static void EditDBUser(this DBUser.DBUser DBuser, EditUser EditUser)
        {
            DBuser.UsrName = EditUser.UsrName;
            DBuser.UsrFirstName = EditUser.UsrFirstName;
            DBuser.UsrLastName = EditUser.UsrLastName;
            DBuser.UsrEmailAddress = EditUser.UsrEmailAddress;
            DBuser.UsrDepartment = EditUser.UsrDepartment;
            DBuser.UsrEnabled = EditUser.UsrEnabled;

            DBuser.UsrDisplayName = EditUser.UsrDisplayName;
        }
    }
}
