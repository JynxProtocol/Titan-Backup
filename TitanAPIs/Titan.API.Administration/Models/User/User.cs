using System;

namespace Titan.Models.User
{
    // transport class representing the client representation of Users
    public class User
    {
        public int UsrID { get; set; }
        public string UsrName { get; set; }
        public Guid? UsrUUID { get; set; }
        public string UsrFirstName { get; set; }
        public string UsrLastName { get; set; }
        public string UsrEmailAddress { get; set; }
        public DateTime? UsrLastLogin { get; set; }
        public int UsrLoginCount { get; set; }
        public string UsrDisplayName { get; set; }
        public string UsrDepartment { get; set; }
        public bool UsrEnabled { get; set; }
    }
}
