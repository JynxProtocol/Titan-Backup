﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class StockRevalueAuditTrail
    {
        public long StockRevalueAuditID { get; set; }
        public int UserNumber { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public long StockItemID { get; set; }
        public decimal OldValue { get; set; }
        public decimal NewValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual StockItem StockItem { get; set; }
    }
}