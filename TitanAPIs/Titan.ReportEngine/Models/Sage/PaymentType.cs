﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            SOPInvoiceCreditArches = new HashSet<SOPInvoiceCreditArch>();
            SOPInvoiceCredits = new HashSet<SOPInvoiceCredit>();
            SOPOrderReturnArches = new HashSet<SOPOrderReturnArch>();
            SOPOrderReturns = new HashSet<SOPOrderReturn>();
        }

        public long PaymentTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SOPInvoiceCreditArch> SOPInvoiceCreditArches { get; set; }
        public virtual ICollection<SOPInvoiceCredit> SOPInvoiceCredits { get; set; }
        public virtual ICollection<SOPOrderReturnArch> SOPOrderReturnArches { get; set; }
        public virtual ICollection<SOPOrderReturn> SOPOrderReturns { get; set; }
    }
}