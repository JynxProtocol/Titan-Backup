﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSWorkPeriod
    {
        public TSWorkPeriod()
        {
            TSTimesheetConfigurations = new HashSet<TSTimesheetConfiguration>();
        }

        public long TSWorkPeriodID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TSTimesheetConfiguration> TSTimesheetConfigurations { get; set; }
    }
}