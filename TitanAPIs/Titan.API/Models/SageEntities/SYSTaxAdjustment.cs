﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSTaxAdjustment
    {
        public long SYSTaxAdjustmentID { get; set; }
        public long SYSTaxPeriodID { get; set; }
        public short VatBoxNumber { get; set; }
        public decimal Amount { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Reason { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSTaxPeriod SYSTaxPeriod { get; set; }
    }
}