﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BOMLine
    {
        public long BOMLineID { get; set; }
        public long BOMID { get; set; }
        public int BOMLineSequence { get; set; }
        public long BOMLineTypeID { get; set; }
        public decimal Quantity { get; set; }
        public long? UnitID { get; set; }
        public string BomLineComment { get; set; }
        public bool ShowCommentInReports { get; set; }
        public string DocumentURL { get; set; }
        public long? StockItemID { get; set; }
        public bool HasComponentRef { get; set; }
        public string ComponentReferences { get; set; }
        public long? BOMCostItemID { get; set; }
        public bool IsCostPerUnit { get; set; }
        public bool UseNewVersionForSubAss { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string SpareText1 { get; set; }
        public string SpareText2 { get; set; }
        public string SpareText3 { get; set; }
        public decimal SpareNumber1 { get; set; }
        public decimal SpareNumber2 { get; set; }
        public decimal SpareNumber3 { get; set; }
        public DateTime? SpareDate1 { get; set; }
        public DateTime? SpareDate2 { get; set; }
        public DateTime? SpareDate3 { get; set; }
        public bool SpareBit1 { get; set; }
        public bool SpareBit2 { get; set; }
        public bool SpareBit3 { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BOM BOM { get; set; }
        public virtual BOMCostItem BOMCostItem { get; set; }
        public virtual BOMLineType BOMLineType { get; set; }
    }
}