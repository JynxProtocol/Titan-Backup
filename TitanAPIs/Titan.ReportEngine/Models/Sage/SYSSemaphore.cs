﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSSemaphore
    {
        public long SYSSemaphoreID { get; set; }
        public short? Semaphore { get; set; }
        public string UserName { get; set; }
        public string Workstation { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? TimeCreated { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}