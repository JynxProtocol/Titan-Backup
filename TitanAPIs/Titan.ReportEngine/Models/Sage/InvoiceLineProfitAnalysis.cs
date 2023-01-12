﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class InvoiceLineProfitAnalysis
    {
        public long InvoiceLineProfitAnalysisID { get; set; }
        public long InvoiceProfitAnalysisID { get; set; }
        public long SOPInvoiceCreditLineID { get; set; }
        public decimal EstimatedCostValue { get; set; }
        public decimal EstProfitPercentOnRev { get; set; }
        public decimal EstProfitPercentOnCost { get; set; }
        public decimal EstimatedProfitValue { get; set; }
        public decimal IssueRate { get; set; }
        public decimal LineQuantity { get; set; }
        public decimal RealisedCostValue { get; set; }
        public decimal RealisedProfitPercentOnCost { get; set; }
        public decimal RealisedProfitPercentOnRev { get; set; }
        public decimal RealisedProfitValue { get; set; }
        public decimal RealisedQuantity { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual InvoiceProfitAnalysis InvoiceProfitAnalysis { get; set; }
        public virtual SOPInvoiceCreditLine SOPInvoiceCreditLine { get; set; }
    }
}