﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BOMMakeupStockOption
    {
        public BOMMakeupStockOption()
        {
            BOMBuilds = new HashSet<BOMBuild>();
            Old200BOMAllocations = new HashSet<Old200BOMAllocation>();
        }

        public long BOMMakeupStockOptionID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BOMBuild> BOMBuilds { get; set; }
        public virtual ICollection<Old200BOMAllocation> Old200BOMAllocations { get; set; }
    }
}