﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class INVInvoiceCredit
    {
        public INVInvoiceCredit()
        {
            INVInvCredNominalItems = new HashSet<INVInvCredNominalItem>();
            INVInvCredTaxItems = new HashSet<INVInvCredTaxItem>();
            INVInvoiceCreditLines = new HashSet<INVInvoiceCreditLine>();
        }

        public long INVInvoiceCreditID { get; set; }
        public long INVBillingAddressID { get; set; }
        public DateTime DocumentDate { get; set; }
        public string SecondReference { get; set; }
        public long INVDeliveryAddressID { get; set; }
        public string DocumentNo { get; set; }
        public long DocumentStatusID { get; set; }
        public decimal InvoicedChargesValue { get; set; }
        public decimal DiscountedTotalGoods { get; set; }
        public decimal InvoicedDiscountValue { get; set; }
        public decimal InvoicedGrossValue { get; set; }
        public decimal InvdGrossValInclSettDisc { get; set; }
        public decimal InvoicedNetValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal DocumentDiscountValue { get; set; }
        public decimal DocumentDiscountPercent { get; set; }
        public decimal SettlementDiscPercent { get; set; }
        public bool TaxOnlyDocument { get; set; }
        public decimal ToBaseCurrencyRate { get; set; }
        public decimal InvoicedCoreTaxValue { get; set; }
        public long INVInvoiceCreditTypeID { get; set; }
        public long? SLCustomerAccountID { get; set; }
        public DateTime? DateCancelled { get; set; }
        public string ReasonCancelled { get; set; }
        public string CancelledBy { get; set; }
        public bool ArePricesTaxInclusive { get; set; }
        public short SettlementDiscountDays { get; set; }
        public decimal InvoicedTaxValue { get; set; }
        public decimal ESVatDiscountAmount { get; set; }
        public decimal InvdNetValInclSettDisc { get; set; }
        public decimal InvdTaxValInclSettDisc { get; set; }
        public decimal InvdCoreTaxValInclSettDisc { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public string PaymentCloudPaymentID { get; set; }
        public string EmailHeader { get; set; }
        public long InvoiceCreditUpdateStatusTypeID { get; set; }
        public long PaymentCloudPaymentStatusTypeID { get; set; }
        public bool IsCIS { get; set; }
        public decimal InvoicedTaxValInclNotional { get; set; }
        public decimal InvoicedCoreTaxValInclNotional { get; set; }

        public virtual INVInvoiceStatus DocumentStatus { get; set; }
        public virtual INVInvoiceCreditAddress INVBillingAddress { get; set; }
        public virtual INVInvoiceCreditAddress INVDeliveryAddress { get; set; }
        public virtual INVInvoiceCreditType INVInvoiceCreditType { get; set; }
        public virtual InvoiceCreditUpdateStatusType InvoiceCreditUpdateStatusType { get; set; }
        public virtual PaymentCloudPaymentStatusType PaymentCloudPaymentStatusType { get; set; }
        public virtual SLCustomerAccount SLCustomerAccount { get; set; }
        public virtual ICollection<INVInvCredNominalItem> INVInvCredNominalItems { get; set; }
        public virtual ICollection<INVInvCredTaxItem> INVInvCredTaxItems { get; set; }
        public virtual ICollection<INVInvoiceCreditLine> INVInvoiceCreditLines { get; set; }
    }
}