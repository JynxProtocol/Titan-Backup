﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TSUserProfile
    {
        public long TSUserProfileID { get; set; }
        public long TSPersonID { get; set; }
        public long TSUserStatusID { get; set; }
        public decimal ExpensesAuthorisationLimit { get; set; }
        public bool CouldAuthorise { get; set; }
        public bool CanEnterTimesheetByProxy { get; set; }
        public bool CanEnterExpensesByProxy { get; set; }
        public string PostingNLCostCentre { get; set; }
        public string PostingNLDepartment { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long? SYSProviderConfigurationID { get; set; }
        public string PayrollProviderName { get; set; }
        public bool CanImportFromPayroll { get; set; }
        public DateTime LastDateWeeklyTSEntry { get; set; }
        public bool WTEAllowAmendChargeRates { get; set; }
        public bool WTEAllowAmendCostRates { get; set; }
        public bool WTEAllowAmendPayRates { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSProviderConfiguration SYSProviderConfiguration { get; set; }
        public virtual TSPerson TSPerson { get; set; }
        public virtual TSUserStatus TSUserStatus { get; set; }
    }
}