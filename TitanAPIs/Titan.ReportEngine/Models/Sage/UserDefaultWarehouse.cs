﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class UserDefaultWarehouse
    {
        public long UserDefaultWarehouseID { get; set; }
        public long SOPUserID { get; set; }
        public long SOPOrderEntryTypeID { get; set; }
        public long? WarehouseID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SOPOrderEntryType SOPOrderEntryType { get; set; }
        public virtual SOPUser SOPUser { get; set; }
        public virtual SOPUserPermission SOPUserNavigation { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}