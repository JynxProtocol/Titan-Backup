// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLStatementLayout
    {
        public NLStatementLayout()
        {
            NLStatementLayoutRows = new HashSet<NLStatementLayoutRow>();
        }

        public long NLStatementLayoutID { get; set; }
        public string LayoutName { get; set; }
        public long NLAccountReportTypeID { get; set; }
        public bool DefaultLayout { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLAccountReportType NLAccountReportType { get; set; }
        public virtual ICollection<NLStatementLayoutRow> NLStatementLayoutRows { get; set; }
    }
}