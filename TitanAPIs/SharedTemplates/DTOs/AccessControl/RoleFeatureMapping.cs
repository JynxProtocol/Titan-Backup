#nullable disable

namespace Titan.Models.AccessControl
{
    public partial class RoleFeatureMapping
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int FeatureID { get; set; }
    }
}
