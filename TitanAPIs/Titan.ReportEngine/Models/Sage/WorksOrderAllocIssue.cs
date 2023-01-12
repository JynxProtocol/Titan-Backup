﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class WorksOrderAllocIssue
    {
        public WorksOrderAllocIssue()
        {
            WorksOrderOpComponents = new HashSet<WorksOrderOpComponent>();
        }

        public long ID { get; set; }
        public long? HeaderID { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal Allocated { get; set; }
        public decimal Issued { get; set; }
        public decimal Required { get; set; }
        public decimal Cancelled { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool? UpdateStock { get; set; }
        public decimal Scrapped { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public decimal TotalIssueCost { get; set; }
        public decimal PostedIssueCost { get; set; }
        public decimal PostedIssueQty { get; set; }
        public double? Sequence { get; set; }
        public long? Locator { get; set; }
        public bool? NewComp { get; set; }
        public string StockUnitName { get; set; }
        public bool FromCompletionWhouse { get; set; }
        public long? MfgAllocLineID { get; set; }
        public double ScrapPercent { get; set; }
        public decimal StockUnitPrecision { get; set; }
        public decimal MultipleOfStockUnit { get; set; }
        public long? StockItemUnitID { get; set; }
        public decimal LineUnitPrecision { get; set; }
        public decimal MultipleOfLineUnit { get; set; }

        public virtual WorksOrder Header { get; set; }
        public virtual ICollection<WorksOrderOpComponent> WorksOrderOpComponents { get; set; }
    }
}