﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SOPInvoiceCredit
    {
        public SOPInvoiceCredit()
        {
            InvoiceProfitAnalyses = new HashSet<InvoiceProfitAnalysis>();
            SOPInvCredNominalItems = new HashSet<SOPInvCredNominalItem>();
            SOPInvCredTaxItems = new HashSet<SOPInvCredTaxItem>();
            SOPInvoiceCreditLines = new HashSet<SOPInvoiceCreditLine>();
        }

        public long SOPInvoiceCreditID { get; set; }
        public long SOPInvoiceCreditTypeID { get; set; }
        public string DocumentNo { get; set; }
        public long DocumentStatusID { get; set; }
        public long CustomerID { get; set; }
        public string SecondReference { get; set; }
        public DateTime DocumentDate { get; set; }
        public bool IsConsolidated { get; set; }
        public bool ArePricesTaxInclusive { get; set; }
        public decimal DocumentDiscountPercent { get; set; }
        public decimal DocumentDiscountValue { get; set; }
        public short SettlementDiscountDays { get; set; }
        public decimal SettlementDiscPercent { get; set; }
        public decimal InvoicedChargesValue { get; set; }
        public decimal DiscountedTotalGoods { get; set; }
        public string FullPaymentText { get; set; }
        public bool PaymentWithOrder { get; set; }
        public long PaymentTypeID { get; set; }
        public decimal PaymentValue { get; set; }
        public string PaymentReference { get; set; }
        public long? PaymentBankAccountID { get; set; }
        public string PaymentNominalAccountRef { get; set; }
        public string PaymentNominalCostCentre { get; set; }
        public string PaymentNominalDepartment { get; set; }
        public decimal InvoicedNetValue { get; set; }
        public decimal InvoicedDiscountValue { get; set; }
        public decimal InvoicedTaxValue { get; set; }
        public decimal InvoicedCoreTaxValue { get; set; }
        public decimal InvoicedGrossValue { get; set; }
        public decimal InvdGrossValInclSettDisc { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime? DateCancelled { get; set; }
        public string ReasonCancelled { get; set; }
        public string CancelledBy { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal InvoicedTaxValInclNotional { get; set; }
        public decimal InvoicedCoreTaxValInclNotional { get; set; }
        public long Source { get; set; }
        public bool TriangularTransaction { get; set; }
        public decimal ESVatDiscountAmount { get; set; }
        public decimal InvdNetValInclSettDisc { get; set; }
        public decimal InvdTaxValInclSettDisc { get; set; }
        public decimal InvdCoreTaxValInclSettDisc { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public string CustomerDocumentNo { get; set; }
        public long? SOPOrderReturnID { get; set; }
        public string PaymentCloudPaymentID { get; set; }
        public string EmailHeader { get; set; }
        public long InvoiceCreditUpdateStatusTypeID { get; set; }
        public long PaymentCloudPaymentStatusTypeID { get; set; }
        public bool IsCIS { get; set; }

        public virtual SLCustomerAccount Customer { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }
        public virtual InvoiceCreditUpdateStatusType InvoiceCreditUpdateStatusType { get; set; }
        public virtual CBAccount PaymentBankAccount { get; set; }
        public virtual PaymentCloudPaymentStatusType PaymentCloudPaymentStatusType { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual SOPInvoiceCreditType SOPInvoiceCreditType { get; set; }
        public virtual SOPOrderReturn SOPOrderReturn { get; set; }
        public virtual SOPInvCredAddress SOPInvCredAddress { get; set; }
        public virtual SOPInvCredDelAddress SOPInvCredDelAddress { get; set; }
        public virtual ICollection<InvoiceProfitAnalysis> InvoiceProfitAnalyses { get; set; }
        public virtual ICollection<SOPInvCredNominalItem> SOPInvCredNominalItems { get; set; }
        public virtual ICollection<SOPInvCredTaxItem> SOPInvCredTaxItems { get; set; }
        public virtual ICollection<SOPInvoiceCreditLine> SOPInvoiceCreditLines { get; set; }
    }
}