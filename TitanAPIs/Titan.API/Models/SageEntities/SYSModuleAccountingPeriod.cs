﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSModuleAccountingPeriod
    {
        public long SYSModuleAccountingPeriodID { get; set; }
        public long SYSAccountingPeriodID { get; set; }
        public long SYSModuleID { get; set; }
        public long SYSModulePostingStatusID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSAccountingPeriod SYSAccountingPeriod { get; set; }
        public virtual SYSModule SYSModule { get; set; }
        public virtual SYSModulePostingStatus SYSModulePostingStatus { get; set; }
    }
}