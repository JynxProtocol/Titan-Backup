﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSTaxPeriodSubmitStatus
    {
        public SYSTaxPeriodSubmitStatus()
        {
            SYSTaxPeriods = new HashSet<SYSTaxPeriod>();
        }

        public long SYSTaxPeriodSubmitStatusID { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SYSTaxPeriod> SYSTaxPeriods { get; set; }
    }
}