﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BomBuildPackageType
    {
        public BomBuildPackageType()
        {
            BomBuildPackages = new HashSet<BomBuildPackage>();
            BomCostSessions = new HashSet<BomCostSession>();
        }

        public long BomBuildPackageTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BomBuildPackage> BomBuildPackages { get; set; }
        public virtual ICollection<BomCostSession> BomCostSessions { get; set; }
    }
}