﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MfgBomAllocHierarchy
    {
        public MfgBomAllocHierarchy()
        {
            InverseBuiltItem = new HashSet<MfgBomAllocHierarchy>();
        }

        public long ID { get; set; }
        public long MFGAllocID { get; set; }
        public string StockCode { get; set; }
        public long? BuiltItemID { get; set; }
        public decimal BuildRequirement { get; set; }
        public decimal StockRequirement { get; set; }

        public virtual MfgBomAllocHierarchy BuiltItem { get; set; }
        public virtual MFGAllocation MFGAlloc { get; set; }
        public virtual ICollection<MfgBomAllocHierarchy> InverseBuiltItem { get; set; }
    }
}