﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MFGAllocBalance
    {
        public long ID { get; set; }
        public long? MfgAllocLineID { get; set; }
        public long? AllocBalanceID { get; set; }

        public virtual AllocationBalance AllocBalance { get; set; }
        public virtual MFGAllocationLine MfgAllocLine { get; set; }
    }
}