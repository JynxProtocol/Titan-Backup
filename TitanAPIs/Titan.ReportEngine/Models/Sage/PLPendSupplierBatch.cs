﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PLPendSupplierBatch
    {
        public PLPendSupplierBatch()
        {
            PLPendSupplierBatchTrans = new HashSet<PLPendSupplierBatchTran>();
        }

        public long PLPendSupplierBatchID { get; set; }
        public long SYSTraderTranTypeID { get; set; }
        public string BatchTitle { get; set; }
        public short NumberOfTransactionsInBatch { get; set; }
        public decimal BatchTotalValue { get; set; }
        public DateTime BatchCreatedDate { get; set; }
        public DateTime? BatchLastAmendedDate { get; set; }
        public string BatchCreatedByUserName { get; set; }
        public short ActualNumberOfTransInBatch { get; set; }
        public decimal ActualBatchTotalValue { get; set; }
        public int UserNumber { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSTraderTranType SYSTraderTranType { get; set; }
        public virtual ICollection<PLPendSupplierBatchTran> PLPendSupplierBatchTrans { get; set; }
    }
}