// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class RTLActivityLog
    {
        public long ActLogId { get; set; }
        public DateTime? ActDateTime { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}