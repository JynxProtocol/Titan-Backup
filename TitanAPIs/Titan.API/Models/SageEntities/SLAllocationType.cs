// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLAllocationType
    {
        public SLAllocationType()
        {
            SLAllocationHeaders = new HashSet<SLAllocationHeader>();
        }

        public long SLAllocationTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SLAllocationHeader> SLAllocationHeaders { get; set; }
    }
}