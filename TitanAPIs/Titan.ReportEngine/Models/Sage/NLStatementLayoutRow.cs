// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLStatementLayoutRow
    {
        public long NLStatementLayoutRowID { get; set; }
        public long NLStatementLayoutID { get; set; }
        public long? NLAccountReportCategoryID { get; set; }
        public long Sequence { get; set; }
        public string Group1 { get; set; }
        public string Group2 { get; set; }
        public string Group3 { get; set; }
        public string Group4 { get; set; }
        public string Group5 { get; set; }
        public string Group6 { get; set; }
        public string Group7 { get; set; }
        public string Group8 { get; set; }
        public string Group9 { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long NLLayoutDebitOrCreditTypeID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLAccountReportCategory NLAccountReportCategory { get; set; }
        public virtual NLLayoutDebitOrCreditType NLLayoutDebitOrCreditType { get; set; }
        public virtual NLStatementLayout NLStatementLayout { get; set; }
    }
}