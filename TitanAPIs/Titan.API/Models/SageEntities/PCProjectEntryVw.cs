﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PCProjectEntryVw
    {
        public long PCProjectEntryID { get; set; }
        public decimal ChargeRateInBaseCurrency { get; set; }
        public decimal CostRateInBaseCurrency { get; set; }
        public long? UnitOfMeasure { get; set; }
        public DateTime? TransactionDate { get; set; }
        public long PCProjectEntryDescriptorID { get; set; }
        public decimal GoodsAmountInBaseCurrency { get; set; }
        public decimal UpliftValueInBaseCurrency { get; set; }
        public string Narrative { get; set; }
        public string NominalAccountNumber { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public long ProjectItemID { get; set; }
        public decimal Quantity { get; set; }
        public string Reference { get; set; }
        public string SecondReference { get; set; }
        public long? SourceEntryID { get; set; }
        public long? NominalAccountingPeriodID { get; set; }
        public decimal TaxAmountInBaseCurrency { get; set; }
        public string TaxCode { get; set; }
        public string TransactionAnalysisCode { get; set; }
        public decimal ValueToBillInBaseCurrency { get; set; }
        public long? TSHumanResourceID { get; set; }
        public long SYSModuleID { get; set; }
        public long URN { get; set; }
        public string User { get; set; }
        public int UserNumber { get; set; }
        public string UnitDescription { get; set; }
        public long EntryTypeID { get; set; }
        public long? ParentEntryID { get; set; }
        public decimal RevenueRateInBaseCurrency { get; set; }
        public decimal TotalOverheadInBaseCurrency { get; set; }
        public string ProjectCode { get; set; }
        public long BillStatusID { get; set; }
        public string ItemName { get; set; }
        public long PCFinancialClassificationID { get; set; }
        public long? Source { get; set; }
        public string InQueryIndicator { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal SettlementDiscountAmount { get; set; }
        public long DocumentCurrencyID { get; set; }
        public decimal ChargeRateInDocCurrency { get; set; }
        public decimal CostRateInDocCurrency { get; set; }
        public decimal GoodsAmountInDocCurrency { get; set; }
        public decimal UpliftValueInDocCurrency { get; set; }
        public decimal ValueToBillInDocCurrency { get; set; }
        public decimal TaxAmountInDocCurrency { get; set; }
        public decimal RevenueRateInDocCurrency { get; set; }
        public decimal TotalOverheadInDocCurrency { get; set; }
        public decimal DiscountAmountInDocCurrency { get; set; }
        public decimal DiscountAmountInBaseCurrency { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string SupplierAccountNumber { get; set; }
        public string StockItemCode { get; set; }
        public long ProjectURN { get; set; }
        public string WIPNominalAccountNumber { get; set; }
        public string WIPNominalCostCentre { get; set; }
        public string WIPNominalDepartment { get; set; }
        public decimal WIPValueInBaseCurrency { get; set; }
        public string FinalisationBillNumber { get; set; }
        public DateTime? FinalisationBillDate { get; set; }
        public bool IsWIPTransaction { get; set; }
        public decimal FinalisedCostValue { get; set; }
        public decimal FinalisedUpliftValue { get; set; }
        public decimal PotentialGoodsAmount { get; set; }
        public decimal ActualGoodsAmount { get; set; }
        public decimal PotentialUpliftValue { get; set; }
        public decimal ActualUpliftValue { get; set; }
        public decimal AmountBilledInBaseCurrency { get; set; }
    }
}