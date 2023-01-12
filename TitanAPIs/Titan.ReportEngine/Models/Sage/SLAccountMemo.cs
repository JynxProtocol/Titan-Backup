﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SLAccountMemo
    {
        public long SLAccountMemoID { get; set; }
        public long SLCustomerAccountID { get; set; }
        public string MemoText { get; set; }
        public string MemoCreatedBy { get; set; }
        public DateTime TimeAndDateMemoCreated { get; set; }
        public bool? Active { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long SYSAccountMemoTypeID { get; set; }
        public DateTime TimeAndDateMemoLastUpdated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SLCustomerAccount SLCustomerAccount { get; set; }
        public virtual SYSAccountMemoType SYSAccountMemoType { get; set; }
    }
}