﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class WorksOrderFinishedProduct
    {
        public long ID { get; set; }
        public long? HeaderID { get; set; }
        public string ProductCode { get; set; }
        public decimal QtyRequired { get; set; }
        public decimal QtyCompleted { get; set; }
        public decimal CostPcnt { get; set; }
        public decimal? QtyOutstanding { get; set; }
        public decimal LineUnitPrecision { get; set; }
        public decimal StockUnitPrecision { get; set; }
        public decimal MultipleOfStockUnit { get; set; }
        public long? StockItemUnitID { get; set; }
        public decimal MultipleOfLineUnit { get; set; }

        public virtual WorksOrder Header { get; set; }
    }
}