﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class WopBuildComponent
    {
        public WopBuildComponent()
        {
            WopBuildComponentIssueLinks = new HashSet<WopBuildComponentIssueLink>();
            WopBuildTraceableComponents = new HashSet<WopBuildTraceableComponent>();
        }

        public long WopBuildComponentID { get; set; }
        public long WopBuildProductID { get; set; }
        public long? WopComponentLineID { get; set; }
        public long? ChildWopBuildID { get; set; }
        public string StockCode { get; set; }
        public decimal Quantity { get; set; }
        public long? UnitID { get; set; }
        public string UnitOfMeasure { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual WopBuild ChildWopBuild { get; set; }
        public virtual WopBuildProduct WopBuildProduct { get; set; }
        public virtual WopComponentLine WopComponentLine { get; set; }
        public virtual ICollection<WopBuildComponentIssueLink> WopBuildComponentIssueLinks { get; set; }
        public virtual ICollection<WopBuildTraceableComponent> WopBuildTraceableComponents { get; set; }
    }
}