﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BomAllocationStatus
    {
        public BomAllocationStatus()
        {
            BomAllocations = new HashSet<BomAllocation>();
        }

        public long BomAllocationStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BomAllocation> BomAllocations { get; set; }
    }
}