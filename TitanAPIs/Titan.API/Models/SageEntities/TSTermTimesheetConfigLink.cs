﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TSTermTimesheetConfigLink
    {
        public long TimesheetClientConfigID { get; set; }
        public long TerminologyID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSTerminology Terminology { get; set; }
        public virtual TSTimesheetClientConfig TimesheetClientConfig { get; set; }
    }
}