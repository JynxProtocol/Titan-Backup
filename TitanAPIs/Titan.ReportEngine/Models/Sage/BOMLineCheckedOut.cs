﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BOMLineCheckedOut
    {
        public long BOMLineCheckedOutID { get; set; }
        public long? BOMID { get; set; }
        public long? BOMCheckedOutID { get; set; }
        public int BOMLineSequence { get; set; }
        public long BOMLineTypeID { get; set; }
        public decimal Quantity { get; set; }
        public long? UnitID { get; set; }
        public string BomLineCheckedOutComment { get; set; }
        public bool ShowCommentInReports { get; set; }
        public string DocumentURL { get; set; }
        public long? StockItemID { get; set; }
        public bool HasComponentReferences { get; set; }
        public string ComponentReferences { get; set; }
        public long? BOMCostItemID { get; set; }
        public bool IsCostPerUnit { get; set; }
        public bool UseNewVersionForSubAss { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BOMCheckedOut BOMCheckedOut { get; set; }
        public virtual BOMCostItem BOMCostItem { get; set; }
        public virtual BOMLineType BOMLineType { get; set; }
    }
}