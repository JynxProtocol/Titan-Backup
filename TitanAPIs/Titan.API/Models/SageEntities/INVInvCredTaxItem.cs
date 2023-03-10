// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class INVInvCredTaxItem
    {
        public long INVInvCredTaxItemID { get; set; }
        public long INVInvoiceCreditID { get; set; }
        public long TaxCodeID { get; set; }
        public decimal CoreGoodsValue { get; set; }
        public decimal CoreDiscountValue { get; set; }
        public decimal CoreTaxValue { get; set; }
        public decimal CoreGrossValue { get; set; }
        public decimal GoodsValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal GrossValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal TaxRate { get; set; }
        public decimal DiscountedTaxValue { get; set; }
        public decimal CoreDiscountedTaxValue { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual INVInvoiceCredit INVInvoiceCredit { get; set; }
        public virtual SYSTaxRate TaxCode { get; set; }
    }
}