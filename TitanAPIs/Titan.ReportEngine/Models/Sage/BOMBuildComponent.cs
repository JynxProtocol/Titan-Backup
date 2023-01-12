﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BOMBuildComponent
    {
        public BOMBuildComponent()
        {
            TraceBOMBuildComps = new HashSet<TraceBOMBuildComp>();
        }

        public long BOMBuildComponentID { get; set; }
        public long BOMBuildID { get; set; }
        public long BOMBuildFinishedItemID { get; set; }
        public decimal Quantity { get; set; }
        public long StockItemID { get; set; }
        public long BinItemID { get; set; }
        public decimal CostContribution { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BOMBuild BOMBuild { get; set; }
        public virtual BOMBuildFinishedItem BOMBuildFinishedItem { get; set; }
        public virtual ICollection<TraceBOMBuildComp> TraceBOMBuildComps { get; set; }
    }
}