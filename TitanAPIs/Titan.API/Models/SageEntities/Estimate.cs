﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class Estimate
    {
        public Estimate()
        {
            EstAttachedDocuments = new HashSet<EstAttachedDocument>();
            EstDrawings = new HashSet<EstDrawing>();
            EstModificationHistories = new HashSet<EstModificationHistory>();
            EstQtyBreaks = new HashSet<EstQtyBreak>();
            EstStages = new HashSet<EstStage>();
        }

        public long ID { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string AccountRef { get; set; }
        public string AccountName { get; set; }
        public DateTime? DateEntered { get; set; }
        public string Notes { get; set; }
        public string Contact { get; set; }
        public string Salesperson { get; set; }
        public string Status { get; set; }
        public string EnteredBy { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal NonStockCost { get; set; }
        public decimal LabourCost { get; set; }
        public decimal MachineCost { get; set; }
        public decimal SetupCost { get; set; }
        public decimal ToolingCost { get; set; }
        public decimal SubContractCost { get; set; }
        public decimal OtherCost { get; set; }
        public decimal MaterialMargin { get; set; }
        public decimal NonStockMargin { get; set; }
        public decimal LabourMargin { get; set; }
        public decimal MachineMargin { get; set; }
        public decimal SetupMargin { get; set; }
        public decimal ToolingMargin { get; set; }
        public decimal SubContractMargin { get; set; }
        public decimal OtherMargin { get; set; }
        public decimal TotalMargin { get; set; }
        public decimal MaterialSelling { get; set; }
        public decimal NonStockSelling { get; set; }
        public decimal LabourSelling { get; set; }
        public decimal MachineSelling { get; set; }
        public decimal SetupSelling { get; set; }
        public decimal ToolingSelling { get; set; }
        public decimal SubContractSelling { get; set; }
        public decimal OtherSelling { get; set; }
        public decimal MaterialMarkUp { get; set; }
        public decimal NonStockMarkUp { get; set; }
        public decimal LabourMarkUp { get; set; }
        public decimal MachineMarkUp { get; set; }
        public decimal SetupMarkUp { get; set; }
        public decimal ToolingMarkUp { get; set; }
        public decimal SubContractMarkUp { get; set; }
        public decimal OtherMarkUp { get; set; }
        public decimal TotalMarkUp { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalSelling { get; set; }
        public DateTime? LastUpdated { get; set; }
        public decimal MaterialProfit { get; set; }
        public decimal NonStockProfit { get; set; }
        public decimal LabourProfit { get; set; }
        public decimal MachineProfit { get; set; }
        public decimal SetupProfit { get; set; }
        public decimal ToolingProfit { get; set; }
        public decimal SubContractProfit { get; set; }
        public decimal OtherProfit { get; set; }
        public decimal TotalProfit { get; set; }
        public string JobNumber { get; set; }
        public bool Printed { get; set; }
        public long? ProspectRecNumber { get; set; }
        public string ReCostedBy { get; set; }
        public DateTime? ReCostedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string CopiedFrom { get; set; }
        public long? JobID { get; set; }
        public string FinProdCode { get; set; }
        public string FinProdDesc { get; set; }
        public decimal FinQty { get; set; }
        public string CustRefNumber { get; set; }
        public decimal OverallDiscPcnt { get; set; }
        public decimal OverallDiscAmnt { get; set; }
        public decimal OHeadRecoveryAmount { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string EstAnalysis_1 { get; set; }
        public string EstAnalysis_2 { get; set; }
        public string EstAnalysis_3 { get; set; }
        public string EstAnalysis_4 { get; set; }
        public string EstAnalysis_5 { get; set; }
        public string Custom_1 { get; set; }
        public string Custom_2 { get; set; }
        public string Custom_3 { get; set; }
        public long? SalesOrderNumber { get; set; }
        public string ImageForWoDocs { get; set; }
        public bool CreateGrnAtComplete { get; set; }
        public short ItemNoDecPlaces { get; set; }
        public bool LineByLineMarkup { get; set; }
        public decimal NumberPieces { get; set; }
        public DateTime? CompOpTimeStamp { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string Address_3 { get; set; }
        public string Address_4 { get; set; }
        public string Address_5 { get; set; }
        public string Memo { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal CurrencyExchangeRate { get; set; }
        public bool FollowUp { get; set; }
        public string SiteAddress_1 { get; set; }
        public string SiteAddress_2 { get; set; }
        public string SiteAddress_3 { get; set; }
        public string SiteAddress_4 { get; set; }
        public string SiteAddress_5 { get; set; }
        public bool Linked { get; set; }
        public long WarehouseID { get; set; }
        public string ProjectNumber { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactMiddleName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPreMigratedData { get; set; }
        public string SalespersonFirstName { get; set; }
        public string SalespersonMiddleName { get; set; }
        public string SalespersonLastName { get; set; }
        public string SalespersonPreMigratedData { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressPostcode { get; set; }
        public string AddressCity { get; set; }
        public string AddressCounty { get; set; }
        public long? AddressCountryID { get; set; }
        public string SiteAddressLine1 { get; set; }
        public string SiteAddressLine2 { get; set; }
        public string SiteAddressLine3 { get; set; }
        public string SiteAddressLine4 { get; set; }
        public string SiteAddressPostcode { get; set; }
        public string SiteAddressCity { get; set; }
        public string SiteAddressCounty { get; set; }
        public long? SiteAddressCountryID { get; set; }
        public long ContactSalutationID { get; set; }
        public long SalespersonSalutationID { get; set; }

        public virtual SYSCountryCode AddressCountry { get; set; }
        public virtual Salutation ContactSalutation { get; set; }
        public virtual Salutation SalespersonSalutation { get; set; }
        public virtual SYSCountryCode SiteAddressCountry { get; set; }
        public virtual ICollection<EstAttachedDocument> EstAttachedDocuments { get; set; }
        public virtual ICollection<EstDrawing> EstDrawings { get; set; }
        public virtual ICollection<EstModificationHistory> EstModificationHistories { get; set; }
        public virtual ICollection<EstQtyBreak> EstQtyBreaks { get; set; }
        public virtual ICollection<EstStage> EstStages { get; set; }
    }
}