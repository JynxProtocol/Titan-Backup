#nullable disable

namespace Titan.Models.AccessControl
{
    public partial class UserRoleMapping
    {
        public int ID { get; set; }
        public int? UsrID { get; set; }
        public int? RoleID { get; set; }
    }
}
