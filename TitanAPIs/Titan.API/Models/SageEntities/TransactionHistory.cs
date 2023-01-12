﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TransactionHistory
    {
        public TransactionHistory()
        {
            StockHistoryShortfalls = new HashSet<StockHistoryShortfall>();
            TraceableTransHistories = new HashSet<TraceableTransHistory>();
        }

        public long TransactionHistoryID { get; set; }
        public long ItemID { get; set; }
        public long TransactionTypeID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Quantity { get; set; }
        public long SourceAreaTypeID { get; set; }
        public string SourceAreaReference { get; set; }
        public string SourceAreaName { get; set; }
        public string Reference { get; set; }
        public string SecondReference { get; set; }
        public string WarehouseName { get; set; }
        public string BinName { get; set; }
        public decimal UnitCostPrice { get; set; }
        public decimal UnitIssuePrice { get; set; }
        public decimal UnitDiscountValue { get; set; }
        public decimal CostValue { get; set; }
        public decimal IssueValue { get; set; }
        public decimal TotalOrderDiscount { get; set; }
        public bool NominalUpdated { get; set; }
        public string AnalysisCode1 { get; set; }
        public string AnalysisCode2 { get; set; }
        public string AnalysisCode3 { get; set; }
        public string UserName { get; set; }
        public long? EntrySourceID { get; set; }
        public long PostingURN { get; set; }
        public short PostingSource { get; set; }
        public int PostingUser { get; set; }
        public DateTime? PostedDate { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string AnalysisCode4 { get; set; }
        public string AnalysisCode5 { get; set; }
        public string AnalysisCode6 { get; set; }
        public string AnalysisCode7 { get; set; }
        public string AnalysisCode8 { get; set; }
        public string AnalysisCode9 { get; set; }
        public string AnalysisCode10 { get; set; }
        public string AnalysisCode11 { get; set; }
        public string AnalysisCode12 { get; set; }
        public string AnalysisCode13 { get; set; }
        public string AnalysisCode14 { get; set; }
        public string AnalysisCode15 { get; set; }
        public string AnalysisCode16 { get; set; }
        public string AnalysisCode17 { get; set; }
        public string AnalysisCode18 { get; set; }
        public string AnalysisCode19 { get; set; }
        public string AnalysisCode20 { get; set; }
        public string VersionNumber { get; set; }
        public string UserRevisionNumber { get; set; }
        public string Memo { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual EntrySource EntrySource { get; set; }
        public virtual StockItem Item { get; set; }
        public virtual SourceAreaType SourceAreaType { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual ICollection<StockHistoryShortfall> StockHistoryShortfalls { get; set; }
        public virtual ICollection<TraceableTransHistory> TraceableTransHistories { get; set; }
    }
}