﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BOMAllocFinishedItem
    {
        public BOMAllocFinishedItem()
        {
            BOMAllocComponents = new HashSet<BOMAllocComponent>();
            InverseIsComponentOf = new HashSet<BOMAllocFinishedItem>();
        }

        public long BOMAllocFinishedItemID { get; set; }
        public long? BOMAllocationID { get; set; }
        public long StockItemID { get; set; }
        public decimal QuantityForMake { get; set; }
        public long? BOMID { get; set; }
        public string BOMVersion { get; set; }
        public bool IsIntermediateItem { get; set; }
        public decimal QuantityBuild { get; set; }
        public decimal QuantityUnallocated { get; set; }
        public long? IsComponentOfID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BOM BOM { get; set; }
        public virtual Old200BOMAllocation BOMAllocation { get; set; }
        public virtual BOMAllocFinishedItem IsComponentOf { get; set; }
        public virtual ICollection<BOMAllocComponent> BOMAllocComponents { get; set; }
        public virtual ICollection<BOMAllocFinishedItem> InverseIsComponentOf { get; set; }
    }
}