﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BLBillHeaderStatus
    {
        public BLBillHeaderStatus()
        {
            BLBills = new HashSet<BLBill>();
        }

        public long BLBillHeaderStatusID { get; set; }
        public long BLBillHeaderStatusTypeID { get; set; }
        public string Assigner { get; set; }
        public string Comments { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BLBillHeaderStatusType BLBillHeaderStatusType { get; set; }
        public virtual ICollection<BLBill> BLBills { get; set; }
    }
}