// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLFinancialReportLayout
    {
        public NLFinancialReportLayout()
        {
            NLFinancialReportRows = new HashSet<NLFinancialReportRow>();
        }

        public long NLFinancialReportLayoutID { get; set; }
        public long NLAccountReportTypeID { get; set; }
        public string Name { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public int BudgetShift { get; set; }
        public bool SystemReport { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLAccountReportType NLAccountReportType { get; set; }
        public virtual ICollection<NLFinancialReportRow> NLFinancialReportRows { get; set; }
    }
}