﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MseProductGroup
    {
        public long MseProductGroupID { get; set; }
        public long ProductGroupID { get; set; }
        public long? MseContactID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool? UseDemandWarehouse { get; set; }
        public bool? UseWOComponentWarehouse { get; set; }
        public long MsmCostHeadingID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MseContact MseContact { get; set; }
        public virtual MsmCostHeading MsmCostHeading { get; set; }
    }
}