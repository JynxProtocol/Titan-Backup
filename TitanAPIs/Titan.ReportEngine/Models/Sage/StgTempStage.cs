﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class StgTempStage
    {
        public StgTempStage()
        {
            StgTempMaterials = new HashSet<StgTempMaterial>();
            StgTempNonStocks = new HashSet<StgTempNonStock>();
            StgTempOperations = new HashSet<StgTempOperation>();
            StgTempOtherExpenses = new HashSet<StgTempOtherExpense>();
        }

        public long ID { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public bool AutoEstimate { get; set; }
        public bool AutoJob { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal NonStockCost { get; set; }
        public decimal LabourCost { get; set; }
        public decimal MachineCost { get; set; }
        public decimal SetupCost { get; set; }
        public decimal ToolingCost { get; set; }
        public decimal SubContractCost { get; set; }
        public decimal OtherCost { get; set; }
        public decimal MaterialMargin { get; set; }
        public decimal NonStockMargin { get; set; }
        public decimal LabourMargin { get; set; }
        public decimal MachineMargin { get; set; }
        public decimal SetupMargin { get; set; }
        public decimal ToolingMargin { get; set; }
        public decimal SubContractMargin { get; set; }
        public decimal OtherMargin { get; set; }
        public decimal TotalMargin { get; set; }
        public decimal MaterialSelling { get; set; }
        public decimal NonStockSelling { get; set; }
        public decimal LabourSelling { get; set; }
        public decimal MachineSelling { get; set; }
        public decimal SetupSelling { get; set; }
        public decimal ToolingSelling { get; set; }
        public decimal SubContractSelling { get; set; }
        public decimal OtherSelling { get; set; }
        public decimal MaterialMarkUp { get; set; }
        public decimal NonStockMarkUp { get; set; }
        public decimal LabourMarkUp { get; set; }
        public decimal MachineMarkUp { get; set; }
        public decimal SetupMarkUp { get; set; }
        public decimal ToolingMarkUp { get; set; }
        public decimal SubContractMarkUp { get; set; }
        public decimal OtherMarkUp { get; set; }
        public decimal TotalMarkUp { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalSelling { get; set; }
        public decimal MaterialProfit { get; set; }
        public decimal NonStockProfit { get; set; }
        public decimal LabourProfit { get; set; }
        public decimal MachineProfit { get; set; }
        public decimal SetupProfit { get; set; }
        public decimal ToolingProfit { get; set; }
        public decimal SubContractProfit { get; set; }
        public decimal OtherProfit { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal OHeadRecoveryAmount { get; set; }

        public virtual ICollection<StgTempMaterial> StgTempMaterials { get; set; }
        public virtual ICollection<StgTempNonStock> StgTempNonStocks { get; set; }
        public virtual ICollection<StgTempOperation> StgTempOperations { get; set; }
        public virtual ICollection<StgTempOtherExpense> StgTempOtherExpenses { get; set; }
    }
}