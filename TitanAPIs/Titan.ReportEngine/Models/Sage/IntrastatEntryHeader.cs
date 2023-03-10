// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class IntrastatEntryHeader
    {
        public IntrastatEntryHeader()
        {
            IntrastatEntries = new HashSet<IntrastatEntry>();
        }

        public long IntrastatEntryHeaderID { get; set; }
        public long IntrastatEntryHeaderStatusID { get; set; }
        public long IntrastatDestinationTypeID { get; set; }
        public string HeaderDescription { get; set; }
        public DateTime HeaderDate { get; set; }
        public string CreatedBy { get; set; }
        public short TaxMonth { get; set; }
        public short TaxYear { get; set; }
        public string TaxRegistrationCode { get; set; }
        public string BranchIDCode { get; set; }
        public string AgentName { get; set; }
        public string AgentTaxRegistrationCode { get; set; }
        public string AgentBranchIDCode { get; set; }
        public bool DeletedEntries { get; set; }

        public virtual IntrastatDestinationType IntrastatDestinationType { get; set; }
        public virtual IntrastatEntryHeaderStatus IntrastatEntryHeaderStatus { get; set; }
        public virtual ICollection<IntrastatEntry> IntrastatEntries { get; set; }
    }
}