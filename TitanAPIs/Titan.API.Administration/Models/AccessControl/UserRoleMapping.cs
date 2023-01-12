#nullable disable

namespace Titan.Models.AccessControl
{
    /// <summary>
    /// A link between a user and a role, representing membership
    /// </summary>
    public partial class UserRoleMapping
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The user that is a member of the role
        /// </summary>
        public int? UsrID { get; set; }
        /// <summary>
        /// The role the user is a member of
        /// </summary>
        public int? RoleID { get; set; }
    }
}
