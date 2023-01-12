﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PCGroupingLevel
    {
        public PCGroupingLevel()
        {
            PCCostGroups = new HashSet<PCCostGroup>();
            PCGroupLevelAnalFieldSettings = new HashSet<PCGroupLevelAnalFieldSetting>();
            PCGroupLevelFeatureSettings = new HashSet<PCGroupLevelFeatureSetting>();
        }

        public long PCGroupingLevelID { get; set; }
        public string Title { get; set; }
        public long PCProjectItemStatusID { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<PCCostGroup> PCCostGroups { get; set; }
        public virtual ICollection<PCGroupLevelAnalFieldSetting> PCGroupLevelAnalFieldSettings { get; set; }
        public virtual ICollection<PCGroupLevelFeatureSetting> PCGroupLevelFeatureSettings { get; set; }
    }
}