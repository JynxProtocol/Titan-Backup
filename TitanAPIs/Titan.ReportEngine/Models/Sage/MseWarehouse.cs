﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MseWarehouse
    {
        public long MseWarehouseID { get; set; }
        public long WarehouseID { get; set; }
        public bool IsComponentSource { get; set; }
        public long? MseContactID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MseContact MseContact { get; set; }
    }
}