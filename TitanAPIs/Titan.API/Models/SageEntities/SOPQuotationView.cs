﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SOPQuotationView
    {
        public string mmssoq_CustomerAccountNumber { get; set; }
        public string mmssoq_QuoteDocumentNo { get; set; }
        public DateTime? mmssoq_DocumentDate { get; set; }
        public string mmssoq_EarlySettlementDiscount { get; set; }
        public double? mmssoq_TotalGrossValueIncSett { get; set; }
        public double? mmssoq_TotalNetValue { get; set; }
        public double? mmssoq_TotalTaxValue { get; set; }
        public double? mmssoq_TotalGrossValue { get; set; }
        public double? mmssoq_DocumentDiscountPercent { get; set; }
        public double? mmssoq_SubtotalGoodsValue { get; set; }
        public double? mmssoq_SubtotalChargesNetValue { get; set; }
        public double? mmssoq_SubtotalChargesTaxValue { get; set; }
        public double? mmssoq_SubtotalDiscountValue { get; set; }
        public string mmssoq_DelPostalName { get; set; }
        public string mmssoq_DelAddressLine1 { get; set; }
        public string mmssoq_DelAddressLine2 { get; set; }
        public string mmssoq_DelAddressLine3 { get; set; }
        public string mmssoq_DelAddressLine4 { get; set; }
        public string mmssoq_DelAddressCity { get; set; }
        public string mmssoq_DelAddressCounty { get; set; }
        public string mmssoq_DelPostCode { get; set; }
        public string mmssoq_AddressLine1 { get; set; }
        public string mmssoq_AddressLine2 { get; set; }
        public string mmssoq_AddressLine3 { get; set; }
        public string mmssoq_AddressLine4 { get; set; }
        public string mmssoq_AddressCity { get; set; }
        public string mmssoq_AddressCounty { get; set; }
        public string mmssoq_PostCode { get; set; }
        public bool mmssoq_UseInvoiceAddress { get; set; }
        public long mmssoq_SOPOrderReturnID { get; set; }
        public string mmssoq_CustomerDocumentNo { get; set; }
        public long mmssoq_DocumentTypeID { get; set; }
        public string mmssoq_CustomerAccountName { get; set; }
        public long? mmssoq_SLCustomerAccountID { get; set; }
        public string mmssoq_CashAcctPostalName { get; set; }
        public string mmssoq_CashAcctAddress1 { get; set; }
        public string mmssoq_CashAcctAddress2 { get; set; }
        public string mmssoq_CashAcctAddress3 { get; set; }
        public string mmssoq_CashAcctAddress4 { get; set; }
        public string mmssoq_CashAcctAddressCity { get; set; }
        public string mmssoq_CashAcctAddressCounty { get; set; }
        public string mmssoq_CashAcctPostCode { get; set; }
    }
}