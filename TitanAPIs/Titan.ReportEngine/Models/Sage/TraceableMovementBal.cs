﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TraceableMovementBal
    {
        public long TraceableMovementBalID { get; set; }
        public long TraceableBinItemID { get; set; }
        public long MovementBalanceID { get; set; }
        public decimal StockLevelIssued { get; set; }
        public decimal OpeningStockLevel { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MovementBalance MovementBalance { get; set; }
        public virtual TraceableBinItem TraceableBinItem { get; set; }
    }
}