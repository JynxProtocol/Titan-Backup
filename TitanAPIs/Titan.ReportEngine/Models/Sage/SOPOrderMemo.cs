﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SOPOrderMemo
    {
        public long SOPOrderMemoID { get; set; }
        public long? SOPOrderReturnID { get; set; }
        public string MemoText { get; set; }
        public string MemoCreatedBy { get; set; }
        public DateTime? TimeAndDateMemoCreated { get; set; }
        public bool Active { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}