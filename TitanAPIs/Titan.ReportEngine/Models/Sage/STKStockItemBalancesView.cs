﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class STKStockItemBalancesView
    {
        public long StockItemID { get; set; }
        public string StockItemCode { get; set; }
        public string StockItemName { get; set; }
        public string ProductGroupCode { get; set; }
        public string ProductGroupDescription { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseDescription { get; set; }
        public decimal MinimumLevel { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal MaximumLevel { get; set; }
        public decimal? QuantityInStock { get; set; }
        public decimal QuantityOnOrder { get; set; }
        public decimal? QuantityAllocated { get; set; }
        public decimal? FreeStock { get; set; }
        public decimal? QuantityUnallocatedLiveSOP { get; set; }
        public decimal? ProjectedAvailableStock { get; set; }
    }
}