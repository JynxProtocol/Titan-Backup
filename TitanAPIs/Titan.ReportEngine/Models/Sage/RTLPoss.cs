﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class RTLPoss
    {
        public long PosId { get; set; }
        public long StoreId { get; set; }
        public string SLAccount { get; set; }
        public string PosType { get; set; }
        public string Location { get; set; }
        public decimal? Version { get; set; }
        public string Description { get; set; }
        public string DataPath { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}