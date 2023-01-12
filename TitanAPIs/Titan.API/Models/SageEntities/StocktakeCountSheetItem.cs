﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class StocktakeCountSheetItem
    {
        public StocktakeCountSheetItem()
        {
            TraceCountSheetItems = new HashSet<TraceCountSheetItem>();
        }

        public long StocktakeCountShtItemID { get; set; }
        public long BinItemID { get; set; }
        public long StocktakeID { get; set; }
        public decimal RecordedQuantityInStock { get; set; }
        public bool ActualQuantityEntered { get; set; }
        public decimal ActualQuantityInStock { get; set; }
        public decimal RecordedTraceUnassigned { get; set; }
        public decimal ActualTraceUnassigned { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal QuantityOnPOPOrder { get; set; }
        public long STKDiscrepancyStatusID { get; set; }
        public string DiscrepancyNarrative { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BinItem BinItem { get; set; }
        public virtual STKDiscrepancyStatus STKDiscrepancyStatus { get; set; }
        public virtual Stocktake Stocktake { get; set; }
        public virtual ICollection<TraceCountSheetItem> TraceCountSheetItems { get; set; }
    }
}