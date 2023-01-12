﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepSopOrderReturnLine
    {
        public long SOPOrderReturnLineID { get; set; }
        public long SOPOrderReturnID { get; set; }
        public short PrintSequenceNumber { get; set; }
        public long LineTypeID { get; set; }
        public string ItemCode { get; set; }
        public bool UseDescription { get; set; }
        public string ItemDescription { get; set; }
        public decimal? LineQuantity { get; set; }
        public decimal? LineTotalValue { get; set; }
        public decimal? LineTaxValue { get; set; }
        public decimal? UnitSellingPrice { get; set; }
        public bool UnitSellPriceOverridden { get; set; }
        public decimal UnitDiscountPercent { get; set; }
        public decimal? UnitDiscountValue { get; set; }
        public bool DiscountPercentSpecified { get; set; }
        public bool UnitDiscountOverridden { get; set; }
        public decimal SellingUnitMultiple { get; set; }
        public decimal PricingUnitMultiple { get; set; }
        public long? TaxCodeID { get; set; }
        public string NominalAccountRef { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public DateTime? RequestedDeliveryDate { get; set; }
        public DateTime? PromisedDeliveryDate { get; set; }
        public string AnalysisCode1 { get; set; }
        public string AnalysisCode2 { get; set; }
        public string AnalysisCode3 { get; set; }
        public string AnalysisCode4 { get; set; }
        public string AnalysisCode5 { get; set; }
        public string AnalysisCode6 { get; set; }
        public bool ShowOnCustomerDocs { get; set; }
        public bool ApplyOrderValueDiscount { get; set; }
        public decimal? AllocatedQuantity { get; set; }
        public bool RepeatOrderPricesFixed { get; set; }
        public decimal? DespatchReceiptQuantity { get; set; }
        public decimal? InvoiceCreditQuantity { get; set; }
        public string PricingUnitDescription { get; set; }
        public string SellingUnitDescription { get; set; }
        public long? POPOrderReturnLineID { get; set; }
        public long BackToBackStatusID { get; set; }
        public bool AdditionalChargeInvoiced { get; set; }
        public bool IsLocked { get; set; }
        public decimal? AvailableForDespatch { get; set; }
        public bool ReadyForInvoicePrint { get; set; }
        public decimal? PostedInvoiceCreditQty { get; set; }
        public long StockItemTypeID { get; set; }
        public decimal? TraceAvailForDespatch { get; set; }
        public decimal? CostPrice { get; set; }
        public bool BOMDetailsOnDespatchNote { get; set; }
        public long? TEMMessageID { get; set; }
        public short? TEMmessageLineNo { get; set; }
        public decimal SellingUnitWeight { get; set; }
        public bool ShowOnDespatchNote { get; set; }
        public long TraceableTypeID { get; set; }
        public long ConfirmationIntentTypeID { get; set; }
        public string DespatchNoteComment { get; set; }
        public string PickingListComment { get; set; }
        public long ShowOnPickingListTypeID { get; set; }
        public long SOPOrderFulfilmentMethodID { get; set; }
        public decimal? QuantityReserved { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
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
        public bool IncludeInMRP { get; set; }
    }
}