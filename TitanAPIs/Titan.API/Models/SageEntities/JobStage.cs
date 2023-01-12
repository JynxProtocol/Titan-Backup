﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class JobStage
    {
        public JobStage()
        {
            JobAllocIssues = new HashSet<JobAllocIssue>();
            JobInvoicesByStages = new HashSet<JobInvoicesByStage>();
            JobMaterials = new HashSet<JobMaterial>();
            JobNonStocks = new HashSet<JobNonStock>();
            JobOperations = new HashSet<JobOperation>();
            JobOtherExpenses = new HashSet<JobOtherExpense>();
        }

        public long ID { get; set; }
        public long? HeaderID { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string EstimateNumber { get; set; }
        public short? PcntComplete { get; set; }
        public decimal EstimatedMaterialCost { get; set; }
        public decimal EstimatedNonStockCost { get; set; }
        public decimal EstimatedLabourCost { get; set; }
        public decimal EstimatedMachineCost { get; set; }
        public decimal EstimatedSetupCost { get; set; }
        public decimal EstimatedToolingCost { get; set; }
        public decimal EstimatedSubCost { get; set; }
        public decimal EstimatedOtherCost { get; set; }
        public decimal EstimatedTotalCost { get; set; }
        public decimal ActualMaterialCost { get; set; }
        public decimal ActualNonStockCost { get; set; }
        public decimal ActualLabourCost { get; set; }
        public decimal ActualMachineCost { get; set; }
        public decimal ActualSetupCost { get; set; }
        public decimal ActualToolingCost { get; set; }
        public decimal ActualSubCost { get; set; }
        public decimal ActualOtherCost { get; set; }
        public decimal ActualTotalCost { get; set; }
        public decimal MaterialSelling { get; set; }
        public decimal NonStockSelling { get; set; }
        public decimal LabourSelling { get; set; }
        public decimal MachineSelling { get; set; }
        public decimal SetupSelling { get; set; }
        public decimal ToolingSelling { get; set; }
        public decimal SubSelling { get; set; }
        public decimal OtherSelling { get; set; }
        public decimal TotalSelling { get; set; }
        public decimal EstMaterialProfit { get; set; }
        public decimal EstNonStockProfit { get; set; }
        public decimal EstLabourProfit { get; set; }
        public decimal EstMachineProfit { get; set; }
        public decimal EstSetupProfit { get; set; }
        public decimal EstToolingProfit { get; set; }
        public decimal EstOtherProfit { get; set; }
        public decimal EstSubProfit { get; set; }
        public decimal EstTotalProfit { get; set; }
        public decimal MaterialActEst { get; set; }
        public decimal NonStockActEst { get; set; }
        public decimal LabourActEst { get; set; }
        public decimal MachineActEst { get; set; }
        public decimal SetupActEst { get; set; }
        public decimal ToolingActEst { get; set; }
        public decimal SubActEst { get; set; }
        public decimal OtherActEst { get; set; }
        public decimal TotalActEst { get; set; }
        public decimal InvoicedTD { get; set; }
        public decimal CreditedTD { get; set; }
        public decimal OSInvoice { get; set; }
        public decimal Profit { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public long? EstStageID { get; set; }
        public string Status { get; set; }
        public decimal EstOHeadAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Sequence { get; set; }
        public DateTime? DueLessSafety { get; set; }
        public decimal QtyDelivered { get; set; }
        public decimal QtyInvoiced { get; set; }

        public virtual Job Header { get; set; }
        public virtual ICollection<JobAllocIssue> JobAllocIssues { get; set; }
        public virtual ICollection<JobInvoicesByStage> JobInvoicesByStages { get; set; }
        public virtual ICollection<JobMaterial> JobMaterials { get; set; }
        public virtual ICollection<JobNonStock> JobNonStocks { get; set; }
        public virtual ICollection<JobOperation> JobOperations { get; set; }
        public virtual ICollection<JobOtherExpense> JobOtherExpenses { get; set; }
    }
}