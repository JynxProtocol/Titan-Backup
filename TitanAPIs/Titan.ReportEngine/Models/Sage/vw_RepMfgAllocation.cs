﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class vw_RepMfgAllocation
    {
        public long MFGAllocationID { get; set; }
        public DateTime? DateTime { get; set; }
        public string BuiltItemStockCode { get; set; }
        public string AllocationReference { get; set; }
        public string AllocationDescription { get; set; }
        public short? MfgModule { get; set; }
        public decimal? BuiltItemQuantity { get; set; }
        public long? BuiltItemBin { get; set; }
    }
}