﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepWorksOrdersAllocIssue
    {
        public long ID { get; set; }
        public long? WoID { get; set; }
        public double? Sequence { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public decimal? Required { get; set; }
        public decimal? Allocated { get; set; }
        public decimal? Issued { get; set; }
        public decimal? Cancelled { get; set; }
        public decimal? Scrapped { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool UpdateStock { get; set; }
        public string Instruction { get; set; }
        public decimal? TotalIssueCost { get; set; }
        public decimal? PostedIssueCost { get; set; }
        public decimal? PostedIssueQty { get; set; }
        public string StockUnitName { get; set; }
        public string FromCompletionWarehouse { get; set; }
        public long? MFGAllocationLineID { get; set; }
    }
}