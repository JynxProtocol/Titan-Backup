﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class InvoiceProfitAnalysis
    {
        public InvoiceProfitAnalysis()
        {
            InvoiceLineProfitAnalyses = new HashSet<InvoiceLineProfitAnalysis>();
        }

        public long InvoiceProfitAnalysisID { get; set; }
        public long SOPInvoiceCreditID { get; set; }
        public decimal EstimatedCostValue { get; set; }
        public decimal EstProfitPercentOnRev { get; set; }
        public decimal EstProfitPercentOnCost { get; set; }
        public decimal EstimatedProfitValue { get; set; }
        public decimal IssueValue { get; set; }
        public decimal RealisedCostValue { get; set; }
        public decimal RealisedIssueValue { get; set; }
        public decimal RealisedProfitPercentOnCost { get; set; }
        public decimal RealisedProfitPercentOnRev { get; set; }
        public decimal RealisedProfitValue { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SOPInvoiceCredit SOPInvoiceCredit { get; set; }
        public virtual ICollection<InvoiceLineProfitAnalysis> InvoiceLineProfitAnalyses { get; set; }
    }
}