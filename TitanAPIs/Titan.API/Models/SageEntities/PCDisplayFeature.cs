// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PCDisplayFeature
    {
        public PCDisplayFeature()
        {
            PCCostItemTypeFeatureSettings = new HashSet<PCCostItemTypeFeatureSetting>();
            PCGroupLevelFeatureSettings = new HashSet<PCGroupLevelFeatureSetting>();
            PCProjectLvlFeatureSettings = new HashSet<PCProjectLvlFeatureSetting>();
        }

        public long PCDisplayFeatureID { get; set; }
        public long? PCDisplayFeatureGroupID { get; set; }
        public string Name { get; set; }
        public bool UseForCostItemType { get; set; }
        public bool UseForGroupingLevel { get; set; }
        public bool UseForProjectLevel { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual PCDisplayFeatureGroup PCDisplayFeatureGroup { get; set; }
        public virtual ICollection<PCCostItemTypeFeatureSetting> PCCostItemTypeFeatureSettings { get; set; }
        public virtual ICollection<PCGroupLevelFeatureSetting> PCGroupLevelFeatureSettings { get; set; }
        public virtual ICollection<PCProjectLvlFeatureSetting> PCProjectLvlFeatureSettings { get; set; }
    }
}