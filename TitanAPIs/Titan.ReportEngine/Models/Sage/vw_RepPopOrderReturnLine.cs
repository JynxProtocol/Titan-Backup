// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class vw_RepPopOrderReturnLine
    {
        public long POPOrderReturnLineID { get; set; }
        public long POPOrderReturnID { get; set; }
        public short PrintSequenceNumber { get; set; }
        public long LineTypeID { get; set; }
        public string ItemCode { get; set; }
        public bool UseDescription { get; set; }
        public string ItemDescription { get; set; }
        public string SupplierPartRef { get; set; }
        public decimal? LineQuantity { get; set; }
        public decimal? LineTotalValue { get; set; }
        public decimal? LineTaxValue { get; set; }
        public decimal? UnitBuyingPrice { get; set; }
        public decimal UnitDiscountPercent { get; set; }
        public decimal? UnitDiscountValue { get; set; }
        public bool DiscountPercentSpecified { get; set; }
        public decimal BuyingUnitMultiple { get; set; }
        public decimal PricingUnitMultiple { get; set; }
        public long? TaxCodeID { get; set; }
        public string NominalAccountRef { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public string AnalysisCode1 { get; set; }
        public string AnalysisCode2 { get; set; }
        public string AnalysisCode3 { get; set; }
        public string AnalysisCode4 { get; set; }
        public string AnalysisCode5 { get; set; }
        public string AnalysisCode6 { get; set; }
        public bool ShowOnSupplierDocs { get; set; }
        public bool ApplyOrderValueDiscount { get; set; }
        public decimal? OnOrderQuantity { get; set; }
        public decimal? ReceiptReturnQuantity { get; set; }
        public decimal? InvoiceCreditQuantity { get; set; }
        public decimal? DisputedInvCredQty { get; set; }
        public string PricingUnitDescription { get; set; }
        public string BuyingUnitDescription { get; set; }
        public bool LinkedToSalesOrderLines { get; set; }
        public decimal? AddChargeInvoiceValue { get; set; }
        public decimal? DisputedAddChargeValue { get; set; }
        public bool IncorrectGRNMatching { get; set; }
        public short DeliverToGroup { get; set; }
        public bool IsLocked { get; set; }
        public bool ForReceiptReturn { get; set; }
        public long StockItemTypeID { get; set; }
        public long TraceableTypeID { get; set; }
        public bool DeliveryDirectFromSupplier { get; set; }
        public long? POPOrdReturnLineDelAddressID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool UpdatePricesAtGoodsReceipt { get; set; }
        public long LandedCostsTypeID { get; set; }
        public decimal? LandedCostsValue { get; set; }
        public string SpareText1 { get; set; }
        public string SpareText2 { get; set; }
        public string SpareText3 { get; set; }
        public decimal SpareNumber1 { get; set; }
        public decimal SpareNumber2 { get; set; }
        public decimal SpareNumber3 { get; set; }
        public DateTime? SpareDate1 { get; set; }
        public DateTime? SpareDate2 { get; set; }
        public DateTime? SpareDate3 { get; set; }
        public bool SpareBit2 { get; set; }
        public bool SpareBit3 { get; set; }
        public bool SpareBit1 { get; set; }
    }
}