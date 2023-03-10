// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLAccountYearValue
    {
        public NLAccountYearValue()
        {
            NLAccountPeriodValues = new HashSet<NLAccountPeriodValue>();
        }

        public long NLAccountYearValueID { get; set; }
        public long SYSFinancialYearID { get; set; }
        public long NLNominalAccountID { get; set; }
        public long NLAnnualBudgetTypeID { get; set; }
        public long? NLAnnualBudgetApportProfileID { get; set; }
        public decimal BudgetValue { get; set; }
        public decimal ClosingBalance { get; set; }
        public decimal AdjustmentAfterYearEndClose { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public decimal OriginalBudgetValue { get; set; }

        public virtual NLAnnualBudgetApportProfile NLAnnualBudgetApportProfile { get; set; }
        public virtual NLAnnualBudgetType NLAnnualBudgetType { get; set; }
        public virtual NLNominalAccount NLNominalAccount { get; set; }
        public virtual SYSFinancialYear SYSFinancialYear { get; set; }
        public virtual ICollection<NLAccountPeriodValue> NLAccountPeriodValues { get; set; }
    }
}