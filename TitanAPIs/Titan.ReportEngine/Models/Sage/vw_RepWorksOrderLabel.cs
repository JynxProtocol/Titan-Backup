﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class vw_RepWorksOrderLabel
    {
        public long ID { get; set; }
        public double Sequence { get; set; }
        public long WoID { get; set; }
        public long CompletionID { get; set; }
        public bool OneOff { get; set; }
        public int SessionID { get; set; }
        public long TraceableItemID { get; set; }
    }
}