﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSFinancialYear
    {
        public SYSFinancialYear()
        {
            NLAccountYearValues = new HashSet<NLAccountYearValue>();
            NLCashFlowReportPeriodHeadings = new HashSet<NLCashFlowReportPeriodHeading>();
            NLCashFlowReports = new HashSet<NLCashFlowReport>();
            NLReportCategoryBudgetYearValues = new HashSet<NLReportCategoryBudgetYearValue>();
            PLSupplierYearValues = new HashSet<PLSupplierYearValue>();
            SLCustomerYearValues = new HashSet<SLCustomerYearValue>();
            SYSAccountingPeriods = new HashSet<SYSAccountingPeriod>();
        }

        public long SYSFinancialYearID { get; set; }
        public DateTime FinancialYearStartDate { get; set; }
        public int YearRelativeToCurrentYear { get; set; }
        public short NumberOfPeriodsInYear { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<NLAccountYearValue> NLAccountYearValues { get; set; }
        public virtual ICollection<NLCashFlowReportPeriodHeading> NLCashFlowReportPeriodHeadings { get; set; }
        public virtual ICollection<NLCashFlowReport> NLCashFlowReports { get; set; }
        public virtual ICollection<NLReportCategoryBudgetYearValue> NLReportCategoryBudgetYearValues { get; set; }
        public virtual ICollection<PLSupplierYearValue> PLSupplierYearValues { get; set; }
        public virtual ICollection<SLCustomerYearValue> SLCustomerYearValues { get; set; }
        public virtual ICollection<SYSAccountingPeriod> SYSAccountingPeriods { get; set; }
    }
}