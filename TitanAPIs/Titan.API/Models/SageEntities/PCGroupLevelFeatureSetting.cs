// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PCGroupLevelFeatureSetting
    {
        public long PCGroupLevelFeatureSettingID { get; set; }
        public long PCGroupingLevelID { get; set; }
        public long PCDisplayFeatureID { get; set; }
        public bool FeatureEnabled { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual PCDisplayFeature PCDisplayFeature { get; set; }
        public virtual PCGroupingLevel PCGroupingLevel { get; set; }
    }
}