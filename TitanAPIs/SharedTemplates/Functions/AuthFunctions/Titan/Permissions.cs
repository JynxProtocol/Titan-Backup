using System.Linq;
using Titan.Data;

namespace Titan.Functions
{
    public class Permissions
    {
        public static string GetUsrDisplayName(string username)
        {
            using AuthEntities _Context = new();
            var userDisplayName = (from user in _Context.Users
                                   where user.UsrName == username
                                   select user.UsrDisplayName).Single();

            return userDisplayName;
        }

        public static string GetUsrDepartment(string username)
        {
            using AuthEntities _Context = new();
            var userDepartment = (from user in _Context.Users
                                  where user.UsrName == username
                                  select user.UsrDepartment).Single();

            return userDepartment;
        }

        public static int GetUsrID(string username)
        {
            using AuthEntities _Context = new();
            var userID = (from user in _Context.Users
                          where user.UsrName == username
                          select user.UsrID).Single();

            return userID;
        }
    }
}