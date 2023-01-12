﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLFinancialReportRow
    {
        public NLFinancialReportRow()
        {
            NLCashFlowReports = new HashSet<NLCashFlowReport>();
            NLFinancialReportCategories = new HashSet<NLFinancialReportCategory>();
        }

        public long NLFinancialReportRowID { get; set; }
        public long NLFinancialReportLayoutID { get; set; }
        public long NLFinancialReportLineTypeID { get; set; }
        public long NLLayoutPositionTypeID { get; set; }
        public long NLLayoutDebitOrCreditTypeID { get; set; }
        public short? ReportRow { get; set; }
        public string Title { get; set; }
        public short? SubtotalGroup { get; set; }
        public bool? PercentageBase { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public int GroupBreak1 { get; set; }
        public int GroupBreak2 { get; set; }
        public int GroupBreak3 { get; set; }
        public int GroupBreak4 { get; set; }
        public int GroupBreak5 { get; set; }
        public int GroupBreak6 { get; set; }
        public int GroupBreak7 { get; set; }
        public int GroupBreak8 { get; set; }
        public int GroupBreak9 { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLFinancialReportLayout NLFinancialReportLayout { get; set; }
        public virtual NLFinancialReportLineType NLFinancialReportLineType { get; set; }
        public virtual NLLayoutDebitOrCreditType NLLayoutDebitOrCreditType { get; set; }
        public virtual NLLayoutPositionType NLLayoutPositionType { get; set; }
        public virtual ICollection<NLCashFlowReport> NLCashFlowReports { get; set; }
        public virtual ICollection<NLFinancialReportCategory> NLFinancialReportCategories { get; set; }
    }
}