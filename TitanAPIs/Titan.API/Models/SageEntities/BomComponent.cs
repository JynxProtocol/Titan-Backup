﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BomComponent
    {
        public long ID { get; set; }
        public long? HeaderID { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public double? Sequence { get; set; }
        public decimal Quantity { get; set; }
        public bool BulkIssue { get; set; }
        public string Location { get; set; }
        public string BinLocation { get; set; }
        public string SubstitutePart { get; set; }
        public bool OnlyForWoCosting { get; set; }
        public long? Locator { get; set; }
        public double ScrapPercent { get; set; }
        public bool? UseHeaderDefaultScrap { get; set; }
        public bool HoldQuantity { get; set; }
        public bool Private { get; set; }
        public decimal LineUnitPrecision { get; set; }
        public decimal StockUnitPrecision { get; set; }
        public decimal LineUnitQuantity { get; set; }
        public decimal MultipleOfStockUnit { get; set; }
        public long? StockItemUnitID { get; set; }
        public decimal MultipleOfLineUnit { get; set; }
        public string UnitofMeasure { get; set; }

        public virtual BomHeader Header { get; set; }
    }
}