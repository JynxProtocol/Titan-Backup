﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BalancePeriod
    {
        public BalancePeriod()
        {
            StockItemPeriodBalances = new HashSet<StockItemPeriodBalance>();
        }

        public long BalancePeriodID { get; set; }
        public string BalancePeriodName { get; set; }
        public DateTime? PeriodOpeningDate { get; set; }
        public DateTime? PeriodClosingDate { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<StockItemPeriodBalance> StockItemPeriodBalances { get; set; }
    }
}