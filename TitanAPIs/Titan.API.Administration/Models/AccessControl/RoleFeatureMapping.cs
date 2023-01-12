#nullable disable

namespace Titan.Models.AccessControl
{
    /// <summary>
    /// A link between a role and a feature, representing granting access
    /// </summary>
    public partial class RoleFeatureMapping
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The role that provides access to features
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// The feature it provides access to
        /// </summary>
        public int FeatureID { get; set; }
    }
}
