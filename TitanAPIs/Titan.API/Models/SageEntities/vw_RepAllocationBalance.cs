﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepAllocationBalance
    {
        public long AllocationBalanceID { get; set; }
        public long? BinItemID { get; set; }
        public long ItemID { get; set; }
        public long WarehouseID { get; set; }
        public long SourceAreaTypeID { get; set; }
        public string RecipientReference { get; set; }
        public string RecipientName { get; set; }
        public decimal? AllocatedQuantity { get; set; }
        public DateTime AllocationDate { get; set; }
        public string Reference { get; set; }
        public string SecondRef { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? PromisedDeliveryDate { get; set; }
        public string OrderPriority { get; set; }
        public long? EntrySourceID { get; set; }
        public decimal? QuantityReserved { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}