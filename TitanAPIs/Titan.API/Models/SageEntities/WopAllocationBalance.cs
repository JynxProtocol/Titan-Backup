// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class WopAllocationBalance
    {
        public long WopAllocationBalanceID { get; set; }
        public long? WopAllocationLineID { get; set; }
        public long AllocationBalanceID { get; set; }
        public decimal AllocationQuantity { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual WopAllocationLine WopAllocationLine { get; set; }
    }
}