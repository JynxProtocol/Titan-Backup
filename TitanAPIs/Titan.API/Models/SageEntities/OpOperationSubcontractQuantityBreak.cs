﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class OpOperationSubcontractQuantityBreak
    {
        public long OpOperationSubcontractQuantityBreakID { get; set; }
        public long OpOperationSubcontractSupplierID { get; set; }
        public decimal? QuantityFrom { get; set; }
        public decimal? QuantityTo { get; set; }
        public decimal Cost { get; set; }
        public int HoursOffsite { get; set; }
        public bool RatePerItem { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual OpOperationSubcontractSupplier OpOperationSubcontractSupplier { get; set; }
    }
}