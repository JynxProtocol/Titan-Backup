﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSCostRateType
    {
        public TSCostRateType()
        {
            TSCostRates = new HashSet<TSCostRate>();
        }

        public long TSCostRateTypeID { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<TSCostRate> TSCostRates { get; set; }
    }
}