﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class RepBomCostingAuditTrail
    {
        public long ID { get; set; }
        public int? SessionID { get; set; }
        public decimal OldStdMatCost { get; set; }
        public decimal OldMatCost { get; set; }
        public decimal OldLabCost { get; set; }
        public decimal OldSetupCost { get; set; }
        public decimal OldMachCost { get; set; }
        public decimal OldSubCost { get; set; }
        public decimal OldToolingCost { get; set; }
        public decimal OldUnitCost { get; set; }
        public decimal OldQtyCostedFor { get; set; }
        public decimal OldSuggSellingPrice { get; set; }
        public decimal StdMatCost { get; set; }
        public decimal MatCost { get; set; }
        public decimal LabCost { get; set; }
        public decimal SetupCost { get; set; }
        public decimal MachCost { get; set; }
        public decimal SubCost { get; set; }
        public decimal ToolingCost { get; set; }
        public decimal UnitCost { get; set; }
        public decimal QtyCostedFor { get; set; }
        public decimal SuggSellingPrice { get; set; }
        public DateTime? CostedOn { get; set; }
        public DateTime? PreviouslyCosted { get; set; }
        public short? Level { get; set; }
        public long? BomID { get; set; }
        public string BomRef { get; set; }
    }
}