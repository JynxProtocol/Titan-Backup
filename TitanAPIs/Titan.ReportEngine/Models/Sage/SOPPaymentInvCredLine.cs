// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SOPPaymentInvCredLine
    {
        public long SOPPaymentInvCredLineID { get; set; }
        public long SOPPaymentID { get; set; }
        public long SOPInvoiceCreditLineID { get; set; }
        public long PaymentInvCredLineTypeID { get; set; }

        public virtual PaymentInvCredLineType PaymentInvCredLineType { get; set; }
        public virtual SOPInvoiceCreditLine SOPInvoiceCreditLine { get; set; }
        public virtual SOPPayment SOPPayment { get; set; }
    }
}