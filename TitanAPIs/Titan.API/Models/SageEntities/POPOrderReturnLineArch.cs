﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class POPOrderReturnLineArch
    {
        public POPOrderReturnLineArch()
        {
            POPInvCreditLineArches = new HashSet<POPInvCreditLineArch>();
            POPRcptReturnLineArches = new HashSet<POPRcptReturnLineArch>();
            POPStandardItemLinkArches = new HashSet<POPStandardItemLinkArch>();
            RequestedDelDateArches = new HashSet<RequestedDelDateArch>();
        }

        public long POPOrderReturnLineID { get; set; }
        public long POPOrderReturnID { get; set; }
        public short PrintSequenceNumber { get; set; }
        public long LineTypeID { get; set; }
        public string ItemCode { get; set; }
        public bool UseDescription { get; set; }
        public string SupplierPartRef { get; set; }
        public decimal LineQuantity { get; set; }
        public decimal LineTotalValue { get; set; }
        public decimal LineTaxValue { get; set; }
        public decimal UnitBuyingPrice { get; set; }
        public decimal UnitDiscountPercent { get; set; }
        public decimal UnitDiscountValue { get; set; }
        public bool DiscountPercentSpecified { get; set; }
        public decimal BuyingUnitMultiple { get; set; }
        public decimal PricingUnitMultiple { get; set; }
        public long TaxCodeID { get; set; }
        public string NominalAccountRef { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public string AnalysisCode1 { get; set; }
        public string AnalysisCode2 { get; set; }
        public string AnalysisCode3 { get; set; }
        public string AnalysisCode4 { get; set; }
        public string AnalysisCode5 { get; set; }
        public string AnalysisCode6 { get; set; }
        public bool? ShowOnSupplierDocs { get; set; }
        public bool ApplyOrderValueDiscount { get; set; }
        public decimal OnOrderQuantity { get; set; }
        public decimal ReceiptReturnQuantity { get; set; }
        public decimal InvoiceCreditQuantity { get; set; }
        public decimal DisputedInvCredQty { get; set; }
        public string PricingUnitDescription { get; set; }
        public string BuyingUnitDescription { get; set; }
        public bool LinkedToSalesOrderLines { get; set; }
        public decimal AddChargeInvoiceValue { get; set; }
        public decimal DisputedAddChargeValue { get; set; }
        public bool IncorrectGRNMatching { get; set; }
        public short? DeliverToGroup { get; set; }
        public bool IsLocked { get; set; }
        public bool ForReceiptReturn { get; set; }
        public long StockItemTypeID { get; set; }
        public long TraceableTypeID { get; set; }
        public bool DeliveryDirectFromSupplier { get; set; }
        public long? POPOrdReturnLineDelAddressID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal LandedCostsValue { get; set; }
        public long LandedCostsTypeID { get; set; }
        public bool UpdatePricesAtGoodsReceipt { get; set; }
        public string SpareText1 { get; set; }
        public string SpareText2 { get; set; }
        public string SpareText3 { get; set; }
        public decimal SpareNumber1 { get; set; }
        public decimal SpareNumber2 { get; set; }
        public decimal SpareNumber3 { get; set; }
        public DateTime? SpareDate1 { get; set; }
        public DateTime? SpareDate2 { get; set; }
        public DateTime? SpareDate3 { get; set; }
        public bool SpareBit1 { get; set; }
        public bool SpareBit2 { get; set; }
        public bool SpareBit3 { get; set; }
        public decimal LineUnitPrecision { get; set; }
        public decimal StockUnitPrecision { get; set; }
        public decimal StockUnitLineQuantity { get; set; }
        public decimal StockUnitOnOrderQuantity { get; set; }
        public decimal StockUnitRcptRtnQuantity { get; set; }
        public decimal StockUnitInvCredQuantity { get; set; }
        public decimal StockUnitDisputedInvCredQty { get; set; }
        public decimal StockUnitMultiple { get; set; }
        public long ConfirmationIntentTypeID { get; set; }
        public string AnalysisCode7 { get; set; }
        public string AnalysisCode8 { get; set; }
        public string AnalysisCode9 { get; set; }
        public string AnalysisCode10 { get; set; }
        public string AnalysisCode11 { get; set; }
        public string AnalysisCode12 { get; set; }
        public string AnalysisCode13 { get; set; }
        public string AnalysisCode14 { get; set; }
        public string AnalysisCode15 { get; set; }
        public string AnalysisCode16 { get; set; }
        public string AnalysisCode17 { get; set; }
        public string AnalysisCode18 { get; set; }
        public string AnalysisCode19 { get; set; }
        public string AnalysisCode20 { get; set; }
        public string ItemDescription { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public long? WarehouseID { get; set; }

        public virtual ConfirmationIntentType ConfirmationIntentType { get; set; }
        public virtual LandedCostsType LandedCostsType { get; set; }
        public virtual OrderReturnLineType LineType { get; set; }
        public virtual POPOrdReturnLineDelAddrArch POPOrdReturnLineDelAddress { get; set; }
        public virtual POPOrderReturnArch POPOrderReturn { get; set; }
        public virtual StockItemType StockItemType { get; set; }
        public virtual TraceableType TraceableType { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<POPInvCreditLineArch> POPInvCreditLineArches { get; set; }
        public virtual ICollection<POPRcptReturnLineArch> POPRcptReturnLineArches { get; set; }
        public virtual ICollection<POPStandardItemLinkArch> POPStandardItemLinkArches { get; set; }
        public virtual ICollection<RequestedDelDateArch> RequestedDelDateArches { get; set; }
    }
}