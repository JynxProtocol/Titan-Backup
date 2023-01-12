﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PLHistRevalAllocationTran
    {
        public long PLHistRevalAllocationTranID { get; set; }
        public long SYSTraderRevalAllocTypeID { get; set; }
        public long PLHistoricalSupplierTranID { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal AllocationValue { get; set; }
        public decimal CoreAllocationValue { get; set; }
        public decimal DocumentToBaseCurrencyRate { get; set; }
        public decimal ExchangeGainLoss { get; set; }
        public bool MultipleAllocation { get; set; }
        public bool NominalUpdated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTransactionMovedToHistory { get; set; }
        public long? NominalAccountingPeriodID { get; set; }
        public long? UniqueReferenceNumber { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSAccountingPeriod NominalAccountingPeriod { get; set; }
        public virtual PLHistoricalSupplierTran PLHistoricalSupplierTran { get; set; }
        public virtual SYSTraderRevalAllocType SYSTraderRevalAllocType { get; set; }
    }
}