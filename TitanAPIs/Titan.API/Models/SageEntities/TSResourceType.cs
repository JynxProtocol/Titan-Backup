﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TSResourceType
    {
        public TSResourceType()
        {
            TSResourceHierarchies = new HashSet<TSResourceHierarchy>();
        }

        public long TSResourceTypeID { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TSResourceHierarchy> TSResourceHierarchies { get; set; }
    }
}