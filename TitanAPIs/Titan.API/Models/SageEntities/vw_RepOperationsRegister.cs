﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepOperationsRegister
    {
        public long ID { get; set; }
        public string ProcessReference { get; set; }
        public string ProcessDescription { get; set; }
        public double? Sequence { get; set; }
        public string Notes { get; set; }
        public decimal? QtyPerRun { get; set; }
        public double? RunTimeHours { get; set; }
        public double? RunTimeMinutes { get; set; }
        public double? SetupTimeHours { get; set; }
        public double? SetupTimeMinutes { get; set; }
        public decimal? SetupRate { get; set; }
        public decimal? SetupTotal { get; set; }
        public string LabourReference { get; set; }
        public string LabourDescription { get; set; }
        public string LabourNotes { get; set; }
        public double? LabourHours { get; set; }
        public double? LabourMinutes { get; set; }
        public decimal? LabourChargeRate { get; set; }
        public decimal? LabourTotal { get; set; }
        public decimal LabourPeople { get; set; }
        public string MachineReference { get; set; }
        public string MachineDescription { get; set; }
        public string MachineNotes { get; set; }
        public double? MachineHours { get; set; }
        public double? MachineMinutes { get; set; }
        public decimal? MachineChargeRate { get; set; }
        public decimal? MachineTotal { get; set; }
        public string KeepTogether { get; set; }
        public double? Overlap { get; set; }
        public double? DelayHours { get; set; }
        public double? DelayMinutes { get; set; }
        public string PieceWork { get; set; }
        public decimal? PieceQuantity { get; set; }
        public decimal? PieceCost { get; set; }
        public string SubContractOp { get; set; }
        public double? SC1From { get; set; }
        public double? SC1To { get; set; }
        public decimal? SC1Cost { get; set; }
        public double? SC1Hours { get; set; }
        public double? SC2From { get; set; }
        public double? SC2To { get; set; }
        public decimal? SC2Cost { get; set; }
        public double? SC2Hours { get; set; }
        public double? SC3From { get; set; }
        public double? SC3To { get; set; }
        public decimal SC3Cost { get; set; }
        public double? SC3Hours { get; set; }
        public double? SC4From { get; set; }
        public double? SC4To { get; set; }
        public decimal SC4Cost { get; set; }
        public double? SC4Hours { get; set; }
        public double? SC5From { get; set; }
        public double? SC5To { get; set; }
        public decimal? SC5Cost { get; set; }
        public double? SC5Hours { get; set; }
        public string SC1PerItem { get; set; }
        public string SC2PerItem { get; set; }
        public string SC3PerItem { get; set; }
        public string SC4PerItem { get; set; }
        public string SC5PerItem { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
        public decimal? ToolingCost { get; set; }
        public decimal? ToolingRepeatQty { get; set; }
        public double? LabSetupHrs { get; set; }
        public double? LabSetupMins { get; set; }
        public string SuppRef { get; set; }
        public string SuppJobRef { get; set; }
        public string NonPrinting { get; set; }
        public int? SubContLeadTime { get; set; }
        public string SubContDetails { get; set; }
        public decimal Shrinkage { get; set; }
        public string IncLabSetup { get; set; }
    }
}