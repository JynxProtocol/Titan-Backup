﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PLTranMemo
    {
        public long PLTranMemoID { get; set; }
        public long PLPostedSupplierTranID { get; set; }
        public string MemoText { get; set; }
        public string MemoCreatedBy { get; set; }
        public DateTime TimeAndDateMemoCreated { get; set; }
        public bool? Active { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime TimeAndDateMemoLastUpdated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual PLPostedSupplierTran PLPostedSupplierTran { get; set; }
    }
}