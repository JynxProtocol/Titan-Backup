﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class EstOpComponent
    {
        public long ID { get; set; }
        public long StageID { get; set; }
        public long OperationID { get; set; }
        public long ComponentID { get; set; }
        public decimal Quantity { get; set; }
        public string UnitofMeasure { get; set; }
        public decimal LineUnitPrecision { get; set; }
        public decimal StockUnitPrecision { get; set; }
        public decimal LineUnitQuantity { get; set; }
        public decimal MultipleOfStockUnit { get; set; }
        public long? StockItemUnitID { get; set; }
        public decimal MultipleOfLineUnit { get; set; }

        public virtual EstMaterial Component { get; set; }
    }
}