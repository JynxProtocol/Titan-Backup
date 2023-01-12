﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class vw_EstimateList
    {
        public long EstimateID { get; set; }
        public string EstimateNumber { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public string InvoiceDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal NumberPieces { get; set; }
        public DateTime? DateEntered { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? DueDate { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountName { get; set; }
        public string FullContactName { get; set; }
        public string ContactSalutation { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactMiddleName { get; set; }
        public string ContactLastName { get; set; }
        public long ContactSalutationID { get; set; }
        public string FullSalespersonName { get; set; }
        public string SalespersonSalutation { get; set; }
        public string SalespersonFirstName { get; set; }
        public string SalespersonMiddleName { get; set; }
        public string SalespersonLastName { get; set; }
        public long SalespersonSalutationID { get; set; }
        public string EstimateStatus { get; set; }
        public string EnteredBy { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal NonStockItemCost { get; set; }
        public decimal LabourCost { get; set; }
        public decimal MachineCost { get; set; }
        public decimal SetupCost { get; set; }
        public decimal ToolingCost { get; set; }
        public decimal SubcontractCost { get; set; }
        public decimal OtherExpensesCost { get; set; }
        public decimal MaterialMargin { get; set; }
        public decimal NonStockItemMargin { get; set; }
        public decimal LabourMargin { get; set; }
        public decimal MachineMargin { get; set; }
        public decimal SetupMargin { get; set; }
        public decimal ToolingMargin { get; set; }
        public decimal SubcontractMargin { get; set; }
        public decimal OtherExpensesMargin { get; set; }
        public decimal TotalMargin { get; set; }
        public decimal MaterialSellingPrice { get; set; }
        public decimal NonStockItemsSellingPrice { get; set; }
        public decimal LabourSellingPrice { get; set; }
        public decimal MachineSellingPrice { get; set; }
        public decimal SetupSellingPrice { get; set; }
        public decimal ToolingSellingPrice { get; set; }
        public decimal SubcontractSellingPrice { get; set; }
        public decimal OtherExpensesSellingPrice { get; set; }
        public decimal MaterialMarkup { get; set; }
        public decimal NonStockItemsMarkup { get; set; }
        public decimal LabourMarkup { get; set; }
        public decimal MachineMarkup { get; set; }
        public decimal SetupMarkup { get; set; }
        public decimal ToolingMarkup { get; set; }
        public decimal SubcontractMarkUp { get; set; }
        public decimal OtherExpensesMarkup { get; set; }
        public decimal TotalMarkup { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalSellingPrice { get; set; }
        public decimal MaterialProfit { get; set; }
        public decimal NonStockItemsProfit { get; set; }
        public decimal LabourProfit { get; set; }
        public decimal MachineProfit { get; set; }
        public decimal SetupProfit { get; set; }
        public decimal ToolingProfit { get; set; }
        public decimal SubcontractProfit { get; set; }
        public decimal OtherExpensesProfit { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal OverallDiscountPercent { get; set; }
        public decimal OverallDiscountAmount { get; set; }
        public decimal OverheadRecoveryAmount { get; set; }
        public bool IsPrinted { get; set; }
        public long? ProspectID { get; set; }
        public int IsProspect { get; set; }
        public string ReCostedBy { get; set; }
        public DateTime? ReCostedDate { get; set; }
        public string CopiedFrom { get; set; }
        public long? JobID { get; set; }
        public string WorksOrderNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string AnalysisCode1 { get; set; }
        public string AnalysisCode2 { get; set; }
        public string AnalysisCode3 { get; set; }
        public string AnalysisCode4 { get; set; }
        public string AnalysisCode5 { get; set; }
        public long? SOPOrderReturnID { get; set; }
        public string SOPDocumentNumber { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal CurrencyExchangeRate { get; set; }
        public bool FlaggedForFollowUp { get; set; }
        public bool IsLinked { get; set; }
        public long WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public string ProjectNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressPostcode { get; set; }
        public string AddressCity { get; set; }
        public string AddressCounty { get; set; }
        public string AddressCountry { get; set; }
        public long? AddressCountryID { get; set; }
        public string SiteAddressLine1 { get; set; }
        public string SiteAddressLine2 { get; set; }
        public string SiteAddressLine3 { get; set; }
        public string SiteAddressLine4 { get; set; }
        public string SiteAddressPostcode { get; set; }
        public string SiteAddressCity { get; set; }
        public string SiteAddressCounty { get; set; }
        public string SiteAddressCountry { get; set; }
        public long? SiteAddressCountryID { get; set; }
        public decimal QtyBreakTotalCost { get; set; }
        public decimal QtyBreakTotalSellingPrice { get; set; }
        public decimal? QtyBreakProfit { get; set; }
        public decimal QtyBreakMargin { get; set; }
        public decimal QtyBreakMarkup { get; set; }
        public decimal ActualCost { get; set; }
        public decimal? ActualProfit { get; set; }
        public decimal? CalculatedCost { get; set; }
        public decimal? CalculatedProfit { get; set; }
        public long SLCustomerAccountID { get; set; }
        public long StockItemID { get; set; }
    }
}