﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSPeriodBalancesLedger
    {
        public SYSPeriodBalancesLedger()
        {
            SYSPeriodBalancesAccounts = new HashSet<SYSPeriodBalancesAccount>();
        }

        public long SYSPeriodBalancesLedgerID { get; set; }
        public long SYSModuleID { get; set; }
        public string WhoRunBy { get; set; }
        public DateTime DateRun { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSModule SYSModule { get; set; }
        public virtual ICollection<SYSPeriodBalancesAccount> SYSPeriodBalancesAccounts { get; set; }
    }
}