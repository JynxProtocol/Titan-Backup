﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class RTLExceptionLog
    {
        public long ExpLogId { get; set; }
        public long ExpTypeId { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public DateTime? ExpDateTime { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}