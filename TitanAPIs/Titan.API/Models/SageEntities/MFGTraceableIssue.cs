﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class MFGTraceableIssue
    {
        public long MFGTraceableIssueID { get; set; }
        public long? TraceableBinItemID { get; set; }
        public decimal Quantity { get; set; }
        public long? MFGIssueID { get; set; }
        public bool UsedInCompletion { get; set; }
        public decimal CompletedQuantity { get; set; }
        public decimal QuantityScrapped { get; set; }

        public virtual MFGIssue MFGIssue { get; set; }
        public virtual TraceableBinItem TraceableBinItem { get; set; }
    }
}