﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class ReportTrialKittingWarehouse
    {
        public long ReportTrialKittingWarehouseID { get; set; }
        public long ReportSessionID { get; set; }
        public string WarehouseName { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ReportSession ReportSession { get; set; }
    }
}