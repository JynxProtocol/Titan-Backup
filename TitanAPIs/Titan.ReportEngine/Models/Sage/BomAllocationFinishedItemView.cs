﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BomAllocationFinishedItemView
    {
        public long? BomAllocationID { get; set; }
        public long WopOrderID { get; set; }
        public string StockCode { get; set; }
        public long MseStockItemID { get; set; }
        public string ReferenceVersion { get; set; }
        public string BuildPackageReference { get; set; }
        public decimal? QuantityOutstanding { get; set; }
        public decimal OriginalQuantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public long StockItemID { get; set; }
        public int AllocationHasModifications { get; set; }
        public bool OrderHasModifications { get; set; }
    }
}