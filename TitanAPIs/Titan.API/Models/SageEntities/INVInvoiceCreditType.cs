// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class INVInvoiceCreditType
    {
        public INVInvoiceCreditType()
        {
            INVInvoiceCredits = new HashSet<INVInvoiceCredit>();
        }

        public long INVInvoiceCreditTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<INVInvoiceCredit> INVInvoiceCredits { get; set; }
    }
}