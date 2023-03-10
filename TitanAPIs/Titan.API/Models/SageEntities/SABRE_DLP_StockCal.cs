// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SABRE_DLP_StockCal
    {
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Stock { get; set; }
        public string BinName { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal MinimumLevel { get; set; }
        public string ProductGroup { get; set; }
        public decimal? _1_Month { get; set; }
        public decimal? _3_Month { get; set; }
        public decimal? _12_Month { get; set; }
        public short LeadTime { get; set; }
        public decimal MinimumOrderQuantity { get; set; }
        public string Warehouse { get; set; }
        public bool UnspecifiedBin { get; set; }
        public decimal? Theo1 { get; set; }
        public decimal? Theo3 { get; set; }
        public decimal? Theo12 { get; set; }
        public decimal? MOQMax { get; set; }
    }
}