﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSUserLogin
    {
        public SYSUserLogin()
        {
            SYSActiveLocks = new HashSet<SYSActiveLock>();
            SYSFeatureAreaUsages = new HashSet<SYSFeatureAreaUsage>();
        }

        public long SYSUserLoginID { get; set; }
        public int UserID { get; set; }
        public DateTime LoginTime { get; set; }
        public string UserName { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public string SessionID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<SYSActiveLock> SYSActiveLocks { get; set; }
        public virtual ICollection<SYSFeatureAreaUsage> SYSFeatureAreaUsages { get; set; }
    }
}