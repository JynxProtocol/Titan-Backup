﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class Sabre_DLP_StockCalSPart
    {
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public decimal? Stock { get; set; }
        public decimal? _3_Month { get; set; }
        public decimal? _1_Month { get; set; }
        public decimal? _12_Month { get; set; }
        public short ActualLeadTime { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal? TheoROL { get; set; }
        public string ProductGroup { get; set; }
        public string BinName { get; set; }
        public decimal? PercentInc { get; set; }
    }
}