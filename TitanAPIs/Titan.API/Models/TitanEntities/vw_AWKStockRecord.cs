﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.API.Models
{
    public partial class vw_AWKStockRecord
    {
        public int AWDID { get; set; }
        public int? DID { get; set; }
        public int? StkID { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public int? RequiredQty { get; set; }
        public string Fault { get; set; }
        public string WorkRequired { get; set; }
        public string RepairDetail { get; set; }
        public decimal? Price { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal MinimumLevel { get; set; }
        public decimal QuantityOnPOPOrder { get; set; }
        public decimal? FreeStock { get; set; }
        public decimal? RepairCost { get; set; }
        public string ProductGroup { get; set; }
        public bool? SabreStock { get; set; }
        public bool? ComAccepted { get; set; }
        public decimal? FreeSabreStock { get; set; }
    }
}