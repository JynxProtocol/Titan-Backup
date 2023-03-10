// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class POPInvCreditLineArch
    {
        public POPInvCreditLineArch()
        {
            POPRcptRtnInvCrdLineArches = new HashSet<POPRcptRtnInvCrdLineArch>();
        }

        public long POPInvoiceCreditLineID { get; set; }
        public string POPInvoiceCreditNo { get; set; }
        public long POPInvCredDisputeID { get; set; }
        public long POPOrderReturnLineID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public decimal InvoiceCreditQuantity { get; set; }
        public DateTime? InvoiceCreditDate { get; set; }
        public decimal LineTotalValue { get; set; }
        public decimal LineTaxValue { get; set; }
        public bool IsDisputed { get; set; }
        public decimal DiscountedUnitPrice { get; set; }
        public decimal ReceiptReturnQuantity { get; set; }
        public long POPInvoiceCreditTypeID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal StockUnitRcptRtnQuantity { get; set; }
        public decimal StockUnitInvCredQuantity { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual POPInvoiceCreditType POPInvoiceCreditType { get; set; }
        public virtual POPOrderReturnLineArch POPOrderReturnLine { get; set; }
        public virtual ICollection<POPRcptRtnInvCrdLineArch> POPRcptRtnInvCrdLineArches { get; set; }
    }
}