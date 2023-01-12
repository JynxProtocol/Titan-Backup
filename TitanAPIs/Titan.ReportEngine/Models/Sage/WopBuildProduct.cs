﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class WopBuildProduct
    {
        public WopBuildProduct()
        {
            WopBuildComponents = new HashSet<WopBuildComponent>();
            WopBuildProductNominalPostings = new HashSet<WopBuildProductNominalPosting>();
            WopBuildTraceableProducts = new HashSet<WopBuildTraceableProduct>();
        }

        public long WopBuildProductID { get; set; }
        public long WopBuildID { get; set; }
        public string StockCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public long UnitID { get; set; }
        public string UnitOfMeasure { get; set; }
        public long? BinItemID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long? MovementBalanceID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual WopBuild WopBuild { get; set; }
        public virtual ICollection<WopBuildComponent> WopBuildComponents { get; set; }
        public virtual ICollection<WopBuildProductNominalPosting> WopBuildProductNominalPostings { get; set; }
        public virtual ICollection<WopBuildTraceableProduct> WopBuildTraceableProducts { get; set; }
    }
}