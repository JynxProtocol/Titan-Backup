﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CostingMethod
    {
        public CostingMethod()
        {
            ProductGroups = new HashSet<ProductGroup>();
        }

        public long CostingMethodID { get; set; }
        public string CostingMethodName { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
    }
}