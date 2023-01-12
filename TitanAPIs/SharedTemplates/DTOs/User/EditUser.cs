using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Models.User
{
    public class EditUser
    {
        public string UsrName { get; set; }
        public string UsrFirstName { get; set; }
        public string UsrLastName { get; set; }
        public string UsrEmailAddress { get; set; }
        public string UsrDisplayName { get; set; }
        public string UsrDepartment { get; set; }
        public bool UsrEnabled { get; set; }
    }
}
