﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MFGTraceableBuiltItem
    {
        public MFGTraceableBuiltItem()
        {
            MFGComponentTraceabilities = new HashSet<MFGComponentTraceability>();
        }

        public long MFGTraceableBuiltItemID { get; set; }
        public long? TraceableItemID { get; set; }
        public decimal Quantity { get; set; }
        public long? MFGWOCompletionID { get; set; }
        public string ItemCode { get; set; }
        public string IdentificationNo { get; set; }

        public virtual MFGWoCompletion MFGWOCompletion { get; set; }
        public virtual TraceableItem TraceableItem { get; set; }
        public virtual ICollection<MFGComponentTraceability> MFGComponentTraceabilities { get; set; }
    }
}