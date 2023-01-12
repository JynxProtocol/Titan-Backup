﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MachMachineResource
    {
        public MachMachineResource()
        {
            MachMachineResourceActivities = new HashSet<MachMachineResourceActivity>();
        }

        public long MachMachineResourceID { get; set; }
        public long MsmCostHeadingID { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Cost Rate
        /// </summary>
        public decimal CostRate { get; set; }
        public long MachMachineResourceGroupID { get; set; }
        public int FullServiceHours { get; set; }
        public int OrdinaryServiceHours { get; set; }
        public int CalibrationHours { get; set; }
        public short? ServiceInterval { get; set; }
        public long ServiceIntervalPeriodID { get; set; }
        public short? CalibrationInterval { get; set; }
        public long CalibrationIntervalPeriodID { get; set; }
        public string Dimensions { get; set; }
        public string Location { get; set; }
        public string OriginalCertificate { get; set; }
        public string Range { get; set; }
        public string AssetSerialNumber { get; set; }
        public string SourceCalibration { get; set; }
        public string Type { get; set; }
        /// <summary>
        /// Overhead Recovery Percentage
        /// </summary>
        public decimal? OverheadRecoveryPercentage { get; set; }
        /// <summary>
        /// Primary Key for Nominal Account
        /// </summary>
        public long? NominalAccountID { get; set; }
        /// <summary>
        /// Nominal Account Number
        /// </summary>
        public string NominalAccountNumber { get; set; }
        /// <summary>
        /// Nominal Cost Centre
        /// </summary>
        public string NominalAccountCostCentre { get; set; }
        /// <summary>
        /// Nominal Department
        /// </summary>
        public string NominalAccountDepartment { get; set; }
        public long? OverheadNominalAccountID { get; set; }
        public string OverheadNominalAccountNumber { get; set; }
        public string OverheadNominalAccountCostCentre { get; set; }
        public string OverheadNominalAccountDepartment { get; set; }
        public bool Archived { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public decimal EfficiencyPercentage { get; set; }
        public bool AllowCostRateOverride { get; set; }
        public string Notes { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MsmPeriodFrequency CalibrationIntervalPeriod { get; set; }
        public virtual MachMachineResourceGroup MachMachineResourceGroup { get; set; }
        public virtual MsmCostHeading MsmCostHeading { get; set; }
        public virtual MsmPeriodFrequency ServiceIntervalPeriod { get; set; }
        public virtual ICollection<MachMachineResourceActivity> MachMachineResourceActivities { get; set; }
    }
}