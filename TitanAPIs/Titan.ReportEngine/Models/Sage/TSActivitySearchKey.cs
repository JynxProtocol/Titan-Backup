﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSActivitySearchKey
    {
        public TSActivitySearchKey()
        {
            TSExpensesClientConfigs = new HashSet<TSExpensesClientConfig>();
            TSTimesheetClientConfigs = new HashSet<TSTimesheetClientConfig>();
        }

        public long TSActivitySearchKeyID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<TSExpensesClientConfig> TSExpensesClientConfigs { get; set; }
        public virtual ICollection<TSTimesheetClientConfig> TSTimesheetClientConfigs { get; set; }
    }
}