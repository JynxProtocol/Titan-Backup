﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SABRE_DLP_VTG_Warehouse
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Warehouse { get; set; }
        public decimal QuantityAllocatedStock { get; set; }
        public decimal QuantityAllocatedSOP { get; set; }
        public decimal QuantityAllocatedBOM { get; set; }
        public decimal? Stock { get; set; }
        public string BinName { get; set; }
        public decimal ReorderLevel { get; set; }
    }
}