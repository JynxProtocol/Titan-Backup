﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PCProjectStatusUpdateVw
    {
        public long PCProjectItemID { get; set; }
        public long PCTopLevelParentID { get; set; }
        public long ProjectStatusID { get; set; }
        public bool? ProjectLevelFeatureEnabled { get; set; }
        public bool? ItemTypeFeatureEnabled { get; set; }
        public bool? GroupLevelFeatureEnabled { get; set; }
    }
}