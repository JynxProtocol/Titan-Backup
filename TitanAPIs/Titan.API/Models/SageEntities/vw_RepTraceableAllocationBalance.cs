// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepTraceableAllocationBalance
    {
        public long TraceableAllocationBalanceID { get; set; }
        public long TraceableBinItemID { get; set; }
        public long? AllocationBalanceID { get; set; }
        public decimal? AllocatedQuantity { get; set; }
        public decimal? DespatchedQuantity { get; set; }
        public decimal? QuantityReserved { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}