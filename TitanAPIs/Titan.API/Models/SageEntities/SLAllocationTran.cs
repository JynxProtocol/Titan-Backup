﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLAllocationTran
    {
        public long SLAllocationTranID { get; set; }
        public long SLAllocationHeaderID { get; set; }
        public long SLPostedCustomerTranID { get; set; }
        public decimal AllocationValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SLAllocationHeader SLAllocationHeader { get; set; }
        public virtual SLPostedCustomerTran SLPostedCustomerTran { get; set; }
    }
}