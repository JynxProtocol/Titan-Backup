﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TraceableTransHistory
    {
        public long TraceableTransHistoryID { get; set; }
        public long TransactionHistoryID { get; set; }
        public long TraceableTransTypeID { get; set; }
        public long TraceableItemID { get; set; }
        public string IdentificationNo { get; set; }
        public string AdditionalReference { get; set; }
        public DateTime? SellByDate { get; set; }
        public DateTime? UseByDate { get; set; }
        public decimal Quantity { get; set; }
        public long TraceableItemStatusID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual TraceableItemStatus TraceableItemStatus { get; set; }
        public virtual TraceableTransType TraceableTransType { get; set; }
        public virtual TransactionHistory TransactionHistory { get; set; }
    }
}