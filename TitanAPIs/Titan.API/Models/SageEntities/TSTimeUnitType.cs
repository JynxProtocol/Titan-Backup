﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TSTimeUnitType
    {
        public TSTimeUnitType()
        {
            TSTimesheetConfigurations = new HashSet<TSTimesheetConfiguration>();
        }

        public long TSTimeUnitTypeID { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TSTimesheetConfiguration> TSTimesheetConfigurations { get; set; }
    }
}