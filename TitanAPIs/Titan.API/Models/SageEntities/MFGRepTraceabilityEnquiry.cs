﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class MFGRepTraceabilityEnquiry
    {
        public long MFGRepTraceabilityEnquiryID { get; set; }
        public long SessionID { get; set; }
        public string Text { get; set; }
        public long ItemDbKey { get; set; }
        public int Type { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}