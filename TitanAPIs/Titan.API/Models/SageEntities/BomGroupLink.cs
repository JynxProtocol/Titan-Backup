﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BomGroupLink
    {
        public long BomGroupLinkID { get; set; }
        public long BomGroupID { get; set; }
        public long BomBuildPackageID { get; set; }
        public decimal Quantity { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BomBuildPackage BomBuildPackage { get; set; }
        public virtual BomGroup BomGroup { get; set; }
    }
}