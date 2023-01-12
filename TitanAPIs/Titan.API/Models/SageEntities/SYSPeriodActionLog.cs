﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSPeriodActionLog
    {
        public long SYSPeriodActionLogID { get; set; }
        public short PeriodNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? OldEndDate { get; set; }
        public string ModuleName { get; set; }
        public string ModulePostingStatusName { get; set; }
        public long SYSPeriodActionID { get; set; }
        public string UserName { get; set; }
        public DateTime DateAndTimeActionPerformed { get; set; }
        public decimal TradingLedgerClosingTotal { get; set; }
        public decimal NominalLedgerControlTotal { get; set; }
        public bool ClosedPeriodReOpened { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSPeriodAction SYSPeriodAction { get; set; }
    }
}