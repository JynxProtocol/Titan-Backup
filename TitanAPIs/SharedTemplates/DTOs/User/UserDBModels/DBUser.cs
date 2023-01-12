using System;
using System.Text;
using Titan.Models;
using TitanAPIFunctions;

#nullable disable

namespace Titan.Models.DBUser
{
    public partial class DBUser
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

        public string UsrPin { get; set; }
        public int? ShowInLookUp { get; set; }
        public string HASH { get; set; }
        public string SALT { get; set; }
        public string UsrResetToken { get; set; }
        public DateTime? UsrResetDateTime { get; set; }
    }
}
