// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLConsolidation
    {
        public NLConsolidation()
        {
            NLConsolidationItems = new HashSet<NLConsolidationItem>();
        }

        public long NLConsolidationID { get; set; }
        public string UniqueIdentifier { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Narrative { get; set; }
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string CurrencyName { get; set; }
        public decimal ExchangeRate { get; set; }

        public virtual ICollection<NLConsolidationItem> NLConsolidationItems { get; set; }
    }
}