// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class WopTraceableIssue
    {
        public long WopTraceableIssueID { get; set; }
        public long WopIssueID { get; set; }
        public decimal Quantity { get; set; }
        public long TraceableItemID { get; set; }
        public string IdentificationNo { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal? ReversedQuantity { get; set; }
        public long TraceableBinItemID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual WopIssue WopIssue { get; set; }
    }
}