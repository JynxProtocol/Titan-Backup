// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.ReportEngine.Models.Titan
{
    public partial class OrderDetail
    {
        public int DetID { get; set; }
        public int? OrdID { get; set; }
        public int? SageStkID { get; set; }
        public long? WONumber { get; set; }
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
        public int? QtyReceived { get; set; }
        public decimal? UnitPrice { get; set; }
        public string SpecialInstruction { get; set; }
        public string SerialNumber { get; set; }
        public string Location { get; set; }
        public long? DetSODID { get; set; }
        public string QuotationNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateRepsReceived { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public int? ExpectedQty { get; set; }
        public int? QtyOutstanding { get; set; }
        public int? ConDetID { get; set; }
    }
}