// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SOPInvoiceCreditType
    {
        public SOPInvoiceCreditType()
        {
            SOPInvoiceCreditArches = new HashSet<SOPInvoiceCreditArch>();
            SOPInvoiceCredits = new HashSet<SOPInvoiceCredit>();
        }

        public long SOPInvoiceCreditTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SOPInvoiceCreditArch> SOPInvoiceCreditArches { get; set; }
        public virtual ICollection<SOPInvoiceCredit> SOPInvoiceCredits { get; set; }
    }
}