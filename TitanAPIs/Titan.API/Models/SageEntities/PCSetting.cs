﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PCSetting
    {
        public long PCSettingID { get; set; }
        public bool UseProjectBudgeting { get; set; }
        public bool UseCostBudgets { get; set; }
        public bool UseRevenueBudgets { get; set; }
        public long PCBudgetLevelID { get; set; }
        public bool UseOverheadAbsorption { get; set; }
        public bool UseProjectSpecificOverheadUpliftRates { get; set; }
        public bool UseSubcontractorSupplierManagement { get; set; }
        public bool UseCommittedCostsAndAccruals { get; set; }
        public string AccrualsNominalAccountNumber { get; set; }
        public string AccrualsNominalAccountCostCentre { get; set; }
        public string AccrualsNominalAccountDepartment { get; set; }
        public bool BillCommittedAccruedCosts { get; set; }
        public bool AutoGenerateProjectCodes { get; set; }
        public bool DisplayProjectStructures { get; set; }
        public long PCCreateProjectPreferenceID { get; set; }
        public bool ValueToBillCanBeAmended { get; set; }
        public bool UsePostOverheadAbsorption { get; set; }
        public bool CombineLinesOnSingleInvoice { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public bool? PostPaymentAsCostElseRevenue { get; set; }
        public bool? PostReceiptAsCostElseRevenue { get; set; }
        public string HierarchySeparator { get; set; }
        public bool UseSOPForRevenueNominal { get; set; }
        public bool UseSOPForCostNominal { get; set; }
        public bool UseWIP { get; set; }
        public bool UseWIPSpecificNominalAccount { get; set; }
        public string WIPNominalAccountNumber { get; set; }
        public string WIPNominalCostCentre { get; set; }
        public string WIPNominalDepartment { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual PCBudgetLevel PCBudgetLevel { get; set; }
        public virtual PCCreateProjectPreference PCCreateProjectPreference { get; set; }
    }
}