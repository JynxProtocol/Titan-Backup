﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.ReportEngine.Models.Titan
{
    public partial class ContractDetail
    {
        public int ConDetID { get; set; }
        public int? ConID { get; set; }
        public int? SageStkID { get; set; }
        public string WONumber { get; set; }
        public string DirtyStockCode { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public string WorkRequired { get; set; }
        public string Colour { get; set; }
        public string Warranty { get; set; }
        public int? LeadTime { get; set; }
        public string DeliveryTerms { get; set; }
        public string DespatchMethod { get; set; }
        public int? DeliveryDays { get; set; }
        public int? QtyRequired { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? IsSerialised { get; set; }
        public int? TotalQty { get; set; }
        public int? TotalQtyReceived { get; set; }
        public string SpecialInstruction { get; set; }
        public string QuotationNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public int? AlternativePart { get; set; }
        public int? APTConID { get; set; }
        public string ParentPart { get; set; }
    }
}