﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TraceCountSheetItem
    {
        public long TraceCountSheetItemID { get; set; }
        public long StocktakeCountShtItemID { get; set; }
        public string IdentificationNo { get; set; }
        public decimal RecordedQuantityInStock { get; set; }
        public bool ActualQuantityEntered { get; set; }
        public decimal ActualQuantityInStock { get; set; }
        public long TraceableBinItemID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual StocktakeCountSheetItem StocktakeCountShtItem { get; set; }
        public virtual TraceableBinItem TraceableBinItem { get; set; }
    }
}