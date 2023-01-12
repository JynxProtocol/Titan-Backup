﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class RTLPricingHistory
    {
        public long PricingHistId { get; set; }
        public long? PricingDefAllId { get; set; }
        public long PrdHierNodeId { get; set; }
        public long? ParentPrdHierNodeId { get; set; }
        public string StockItemCode { get; set; }
        public string SupplierCode { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Margin { get; set; }
        public decimal? Markup { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreateDate { get; set; }
        public string PriceBand { get; set; }
        public decimal LandedCosts { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}