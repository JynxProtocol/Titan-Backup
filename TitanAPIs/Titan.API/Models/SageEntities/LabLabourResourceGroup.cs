﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class LabLabourResourceGroup
    {
        public LabLabourResourceGroup()
        {
            LabLabourResources = new HashSet<LabLabourResource>();
        }

        public long LabLabourResourceGroupID { get; set; }
        public long MsmCostHeadingID { get; set; }
        public string GroupReference { get; set; }
        public string Description { get; set; }
        public bool PieceWork { get; set; }
        public decimal? PieceWorkQuantity { get; set; }
        public decimal CostRate { get; set; }
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
        public bool Archived { get; set; }
        /// <summary>
        /// Overhead Recovery Percentage
        /// </summary>
        public decimal OverheadRecoveryPercentage { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal EfficiencyPercentage { get; set; }
        public long? OverheadNominalAccountID { get; set; }
        public string OverheadNominalAccountNumber { get; set; }
        public string OverheadNominalAccountCostCentre { get; set; }
        public string OverheadNominalAccountDepartment { get; set; }
        public bool AllowCostRateOverride { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MsmCostHeading MsmCostHeading { get; set; }
        public virtual ICollection<LabLabourResource> LabLabourResources { get; set; }
    }
}