﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class WopBuildTraceableComponent
    {
        public WopBuildTraceableComponent()
        {
            WopBuildComponentTraceabilities = new HashSet<WopBuildComponentTraceability>();
        }

        public long WopBuildTraceableComponentID { get; set; }
        public long WopBuildComponentID { get; set; }
        public long TraceableItemID { get; set; }
        public string IdentificationNo { get; set; }
        public decimal Quantity { get; set; }
        public long UnitID { get; set; }
        public string UnitOfMeasure { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual WopBuildComponent WopBuildComponent { get; set; }
        public virtual ICollection<WopBuildComponentTraceability> WopBuildComponentTraceabilities { get; set; }
    }
}