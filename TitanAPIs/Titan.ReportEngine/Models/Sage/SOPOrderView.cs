﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SOPOrderView
    {
        public string mmssoo_CustomerAccountNumber { get; set; }
        public string mmssoo_OrderDocumentNo { get; set; }
        public DateTime? mmssoo_DocumentDate { get; set; }
        public string mmssoo_EarlySettlementDiscount { get; set; }
        public double? mmssoo_TotalGrossValueIncSett { get; set; }
        public double? mmssoo_TotalNetValue { get; set; }
        public double? mmssoo_TotalTaxValue { get; set; }
        public double? mmssoo_TotalGrossValue { get; set; }
        public double? mmssoo_DocumentDiscountPercent { get; set; }
        public double? mmssoo_SubtotalGoodsValue { get; set; }
        public double? mmssoo_SubtotalChargesNetValue { get; set; }
        public double? mmssoo_SubtotalChargesTaxValue { get; set; }
        public double? mmssoo_SubtotalDiscountValue { get; set; }
        public string mmssoo_DelPostalName { get; set; }
        public string mmssoo_DelAddressLine1 { get; set; }
        public string mmssoo_DelAddressLine2 { get; set; }
        public string mmssoo_DelAddressLine3 { get; set; }
        public string mmssoo_DelAddressLine4 { get; set; }
        public string mmssoo_DelAddressCity { get; set; }
        public string mmssoo_DelAddressCounty { get; set; }
        public string mmssoo_DelPostCode { get; set; }
        public string mmssoo_AddressLine1 { get; set; }
        public string mmssoo_AddressLine2 { get; set; }
        public string mmssoo_AddressLine3 { get; set; }
        public string mmssoo_AddressLine4 { get; set; }
        public string mmssoo_AddressCity { get; set; }
        public string mmssoo_AddressCounty { get; set; }
        public string mmssoo_PostCode { get; set; }
        public bool mmssoo_UseInvoiceAddress { get; set; }
        public long mmssoo_SOPOrderReturnID { get; set; }
        public string mmssoo_CustomerDocumentNo { get; set; }
        public long mmssoo_DocumentTypeID { get; set; }
        public string mmssoo_CustomerAccountName { get; set; }
        public long? mmssoo_SLCustomerAccountID { get; set; }
    }
}