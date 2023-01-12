﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TSTimesheetClientConfig
    {
        public TSTimesheetClientConfig()
        {
            TSTermTimesheetConfigLinks = new HashSet<TSTermTimesheetConfigLink>();
        }

        public long TSTimesheetClientConfigID { get; set; }
        public bool? SelectCostRates { get; set; }
        public bool DisplayCostValues { get; set; }
        public bool? SelectChargeRates { get; set; }
        public bool DisplayChargeValues { get; set; }
        public bool? SelectPayRates { get; set; }
        public bool DisplayPayValues { get; set; }
        public long TSUserSearchKeyID { get; set; }
        public long TSActivitySearchKeyID { get; set; }
        public long TSSubmissionFrequencyTypeID { get; set; }
        public int ZeroSubmissionActivityRetension { get; set; }
        public bool? EnableCostRates { get; set; }
        public bool? EnableChargeRates { get; set; }
        public bool? EnablePayRates { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual TSActivitySearchKey TSActivitySearchKey { get; set; }
        public virtual TSSubmissionFrequencyType TSSubmissionFrequencyType { get; set; }
        public virtual TSUserSearchKey TSUserSearchKey { get; set; }
        public virtual ICollection<TSTermTimesheetConfigLink> TSTermTimesheetConfigLinks { get; set; }
    }
}