// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class ReportUndoAllocationFinishedItem
    {
        public long ReportUndoAllocationFinishedItemID { get; set; }
        public long ReportSessionID { get; set; }
        public string AllocationReference { get; set; }
        public string AllocationNumber { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal QuantityUnallocated { get; set; }
        public decimal QuantityRemaining { get; set; }
        public string StockUnitName { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ReportSession ReportSession { get; set; }
    }
}