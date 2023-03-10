// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class JobOperation
    {
        public JobOperation()
        {
            JobActualOpTimes = new HashSet<JobActualOpTime>();
        }

        public long ID { get; set; }
        public long? HeaderID { get; set; }
        public string ProcessReference { get; set; }
        public string ProcessDescription { get; set; }
        public double? Sequence { get; set; }
        public string LabourReference { get; set; }
        public string LabourDescription { get; set; }
        public string MachineReference { get; set; }
        public string MachineDescription { get; set; }
        public double EstLabourMinutes { get; set; }
        public double EstMachineMinutes { get; set; }
        public double EstSetupMinutes { get; set; }
        public double EstSubMinutes { get; set; }
        public decimal InProgress { get; set; }
        public decimal QtyComplete { get; set; }
        public double ActLabourMinutes { get; set; }
        public double ActMachineMinutes { get; set; }
        public double ActSetupMinutes { get; set; }
        public double ActSubMinutes { get; set; }
        public decimal ActLabourCost { get; set; }
        public decimal ActMachineCost { get; set; }
        public decimal ActSetupCost { get; set; }
        public decimal ActToolingCost { get; set; }
        public decimal ActSubCost { get; set; }
        public long? JobID { get; set; }
        public decimal LabourRate { get; set; }
        public decimal MachineRate { get; set; }
        public decimal SetupRate { get; set; }
        public bool PieceWork { get; set; }
        public decimal PieceWorkQty { get; set; }
        public decimal PieceWorkRate { get; set; }
        public bool SubContract { get; set; }
        public string Notes { get; set; }
        public bool NonPrinting { get; set; }
        public int? SubContLeadTime { get; set; }
        public string SubContDetails { get; set; }
        public decimal NumberPieces { get; set; }
        public decimal Shrinkage { get; set; }
        public bool IncLabSetup { get; set; }
        public bool CertRequired { get; set; }
        public long EstOpID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? SetupStart { get; set; }
        public decimal QtyPerRun { get; set; }
        public double? RunTimeHours { get; set; }
        public double? RunTimeMinutes { get; set; }
        public double? SetupTimeHours { get; set; }
        public double? SetupTimeMinutes { get; set; }
        public decimal SetupTotal { get; set; }
        public string LabourNotes { get; set; }
        public double? LabourHours { get; set; }
        public double? LabourMinutes { get; set; }
        public decimal LabourTotal { get; set; }
        public decimal LabourPeople { get; set; }
        public string MachineNotes { get; set; }
        public double? MachineHours { get; set; }
        public double? MachineMinutes { get; set; }
        public decimal MachineTotal { get; set; }
        public bool KeepTogether { get; set; }
        public double? Overlap { get; set; }
        public double? DelayHours { get; set; }
        public double? DelayMinutes { get; set; }
        public decimal SC1Cost { get; set; }
        public double? SC1From { get; set; }
        public double? SC1Hours { get; set; }
        public bool SC1PerItem { get; set; }
        public double? SC1To { get; set; }
        public decimal SC2Cost { get; set; }
        public double? SC2From { get; set; }
        public double? SC2Hours { get; set; }
        public bool SC2PerItem { get; set; }
        public double? SC2To { get; set; }
        public decimal SC3Cost { get; set; }
        public double? SC3From { get; set; }
        public double? SC3Hours { get; set; }
        public bool SC3PerItem { get; set; }
        public double? SC3To { get; set; }
        public decimal SC4Cost { get; set; }
        public double? SC4From { get; set; }
        public double? SC4Hours { get; set; }
        public bool SC4PerItem { get; set; }
        public double? SC4To { get; set; }
        public decimal SC5Cost { get; set; }
        public double? SC5From { get; set; }
        public double? SC5Hours { get; set; }
        public bool SC5PerItem { get; set; }
        public double? SC5To { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public decimal ProcessTotal { get; set; }
        public decimal ToolingCost { get; set; }
        public decimal ToolingRepeatQty { get; set; }
        public decimal CalcLabourCost { get; set; }
        public decimal CalcMachineCost { get; set; }
        public decimal CalcToolingCost { get; set; }
        public decimal CalcSubContractCost { get; set; }
        public double? LabSetupHrs { get; set; }
        public double? LabSetupMins { get; set; }
        public string SuppRef { get; set; }
        public string SuppJobRef { get; set; }
        public decimal ActSubContVariance { get; set; }

        public virtual JobStage Header { get; set; }
        public virtual ICollection<JobActualOpTime> JobActualOpTimes { get; set; }
    }
}