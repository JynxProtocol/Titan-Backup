﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TSResourceHierarchyPerson
    {
        public long TSResourceHierarchyPersonID { get; set; }
        public long TSResourceHierarchyID { get; set; }
        public long TSPersonID { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual TSPerson TSPerson { get; set; }
        public virtual TSResourceHierarchy TSResourceHierarchy { get; set; }
    }
}