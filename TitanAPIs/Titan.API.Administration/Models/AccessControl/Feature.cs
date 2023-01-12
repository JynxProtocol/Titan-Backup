#nullable disable

namespace Titan.Models.AccessControl
{
    /// <summary>
    /// A single feature of Titan to which access can be restricted
    /// </summary>
    public partial class Feature
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the feature
        /// </summary>
        public string FeatureName { get; set; }
        /// <summary>
        /// The area that the feature is a part of
        /// </summary>
        public string FeatureArea { get; set; }
    }
}
