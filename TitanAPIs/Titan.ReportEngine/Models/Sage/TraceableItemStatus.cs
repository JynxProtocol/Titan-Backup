﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TraceableItemStatus
    {
        public TraceableItemStatus()
        {
            TraceableItemArches = new HashSet<TraceableItemArch>();
            TraceableItems = new HashSet<TraceableItem>();
            TraceableTransArchives = new HashSet<TraceableTransArchive>();
            TraceableTransHistories = new HashSet<TraceableTransHistory>();
        }

        public long TraceableItemStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TraceableItemArch> TraceableItemArches { get; set; }
        public virtual ICollection<TraceableItem> TraceableItems { get; set; }
        public virtual ICollection<TraceableTransArchive> TraceableTransArchives { get; set; }
        public virtual ICollection<TraceableTransHistory> TraceableTransHistories { get; set; }
    }
}