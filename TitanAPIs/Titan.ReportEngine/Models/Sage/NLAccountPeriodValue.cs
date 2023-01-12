﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLAccountPeriodValue
    {
        public long NLAccountPeriodValueID { get; set; }
        public long SYSAccountingPeriodID { get; set; }
        public long NLNominalAccountID { get; set; }
        public long NLAccountYearValueID { get; set; }
        public decimal? PercentageOfAnnualBudget { get; set; }
        public decimal BudgetValue { get; set; }
        public decimal ActualValue { get; set; }
        public decimal AdjustmentAfterYearEndClose { get; set; }
        public decimal ConsolidatedAmount { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public decimal OriginalBudgetValue { get; set; }

        public virtual NLAccountYearValue NLAccountYearValue { get; set; }
        public virtual NLNominalAccount NLNominalAccount { get; set; }
        public virtual SYSAccountingPeriod SYSAccountingPeriod { get; set; }
    }
}