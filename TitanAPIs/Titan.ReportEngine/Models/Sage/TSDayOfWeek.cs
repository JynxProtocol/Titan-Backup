﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSDayOfWeek
    {
        public TSDayOfWeek()
        {
            TSTimesheetConfigurations = new HashSet<TSTimesheetConfiguration>();
        }

        public long TSDayOfWeekID { get; set; }
        public string Description { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<TSTimesheetConfiguration> TSTimesheetConfigurations { get; set; }
    }
}