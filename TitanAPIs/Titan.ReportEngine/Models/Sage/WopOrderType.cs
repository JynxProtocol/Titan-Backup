﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class WopOrderType
    {
        public WopOrderType()
        {
            WopOrders = new HashSet<WopOrder>();
        }

        public long WopOrderTypeID { get; set; }
        public string Name { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<WopOrder> WopOrders { get; set; }
    }
}