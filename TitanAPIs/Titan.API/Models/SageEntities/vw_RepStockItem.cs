﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepStockItem
    {
        public long ItemID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long ProductGroupID { get; set; }
        public string Description { get; set; }
        public bool UseDescriptionOnDocs { get; set; }
        public long TaxCodeID { get; set; }
        public decimal? StandardCost { get; set; }
        public decimal? SOPItemPrice { get; set; }
        public long? StockItemStatusID { get; set; }
        public DateTime? InactiveDate { get; set; }
        public string Manufacturer { get; set; }
        public string PartNumber { get; set; }
        public short StocktakeCyclePeriod { get; set; }
        public string CommodityCode { get; set; }
        public decimal Weight { get; set; }
        public bool SuppressNetMass { get; set; }
        public string StockUnitName { get; set; }
        public string BaseUnitName { get; set; }
        public decimal StockMultOfBaseUnit { get; set; }
        public string Barcode { get; set; }
        public DateTime? StdCostVarianceLastReset { get; set; }
        public decimal? AverageBuyingPrice { get; set; }
        public long TraceableTypeID { get; set; }
        public bool SaleFromSingleBatch { get; set; }
        public bool AllowDuplicateNumbers { get; set; }
        public bool UsesAlternativeRef { get; set; }
        public bool UsesSellByDate { get; set; }
        public bool UsesUseByDate { get; set; }
        public bool RecordNosOnGoodsReceived { get; set; }
        public DateTime? LastArchivedUpTo { get; set; }
        public decimal? FreeStockQuantity { get; set; }
        public long? BOMItemTypeID { get; set; }
        public long SOPOrderFulfilmentMethodID { get; set; }
        public string DefaultDespatchNoteComment { get; set; }
        public string DefaultPickingListComment { get; set; }
        public decimal? QuantityReserved { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime? LastTraceArchivedUpTo { get; set; }
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
        public bool SpareBit1 { get; set; }
        public bool SpareBit2 { get; set; }
        public bool SpareBit3 { get; set; }
        public bool AllowSalesOrder { get; set; }
        public long STKAutoGenerateOptionTypeID { get; set; }
        public string AutoGeneratePrefix { get; set; }
        public long AutoGenerateNextNumber { get; set; }
        public long STKLabelPrintingOptionTypeID { get; set; }
        public long STKFulfilmentSequenceTypeID { get; set; }
        public int ShelfLife { get; set; }
        public long? STKShelfLifeTypeID { get; set; }
        public long STKAutoGenerateSeparatorID { get; set; }
        public int AutoGeneratePadding { get; set; }
        public bool AllowOutOfDate { get; set; }
    }
}