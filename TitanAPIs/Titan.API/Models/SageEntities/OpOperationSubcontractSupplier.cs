﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class OpOperationSubcontractSupplier
    {
        public OpOperationSubcontractSupplier()
        {
            OpOperationSubcontractQuantityBreaks = new HashSet<OpOperationSubcontractQuantityBreak>();
        }

        public long OpOperationSubcontractSupplierID { get; set; }
        public long OpOperationID { get; set; }
        public bool DefaultSupplier { get; set; }
        public long? SupplierID { get; set; }
        public string AccountNumber { get; set; }
        public string OrderReference { get; set; }
        public int LeadTime { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public long MsmCostHeadingID { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public string OrderDetails { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MsmCostHeading MsmCostHeading { get; set; }
        public virtual OpOperation OpOperation { get; set; }
        public virtual ICollection<OpOperationSubcontractQuantityBreak> OpOperationSubcontractQuantityBreaks { get; set; }
    }
}