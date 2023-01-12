﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSCostRate
    {
        public TSCostRate()
        {
            TSHumanResourceCostRateLinks = new HashSet<TSHumanResourceCostRateLink>();
            TSResourceCostRateLinks = new HashSet<TSResourceCostRateLink>();
            TSTimeRecords = new HashSet<TSTimeRecord>();
        }

        public long TSCostRateID { get; set; }
        public long? TSCostRateTypeID { get; set; }
        public decimal CostRateValue { get; set; }
        public long? TSChargeRateTypeID { get; set; }
        public bool IsDefault { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool IsFromPayRate { get; set; }
        public long? TSPayRateTypeID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual TSChargeRateType TSChargeRateType { get; set; }
        public virtual TSCostRateType TSCostRateType { get; set; }
        public virtual TSPayRateType TSPayRateType { get; set; }
        public virtual ICollection<TSHumanResourceCostRateLink> TSHumanResourceCostRateLinks { get; set; }
        public virtual ICollection<TSResourceCostRateLink> TSResourceCostRateLinks { get; set; }
        public virtual ICollection<TSTimeRecord> TSTimeRecords { get; set; }
    }
}