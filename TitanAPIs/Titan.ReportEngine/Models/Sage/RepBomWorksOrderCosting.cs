﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class RepBomWorksOrderCosting
    {
        public long ID { get; set; }
        public int? SessionID { get; set; }
        public string BomReference { get; set; }
        public string WorksOrderNumber { get; set; }
        public string RecordType { get; set; }
        public double? Sequence { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public decimal SetupCost { get; set; }
        public decimal LabourCost { get; set; }
        public decimal MachineCost { get; set; }
        public decimal ToolingCost { get; set; }
        public string PieceWork { get; set; }
        public string UnitOfMeasure { get; set; }
        public long? BomID { get; set; }
        public long? WoID { get; set; }
        public decimal QtyCostedFor { get; set; }
        public long OpID { get; set; }
        public long ActualBomID { get; set; }
        public bool Rolledup { get; set; }
        public bool SubContract { get; set; }
    }
}