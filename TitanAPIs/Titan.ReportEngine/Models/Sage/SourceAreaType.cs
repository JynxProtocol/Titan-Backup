﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SourceAreaType
    {
        public SourceAreaType()
        {
            AllocationBalances = new HashSet<AllocationBalance>();
            MovementBalances = new HashSet<MovementBalance>();
            TraceableItemArches = new HashSet<TraceableItemArch>();
            TraceableItems = new HashSet<TraceableItem>();
            TransactionArchives = new HashSet<TransactionArchive>();
            TransactionHistories = new HashSet<TransactionHistory>();
        }

        public long SourceAreaTypeID { get; set; }
        public string SourceAreaTypeName { get; set; }

        public virtual ICollection<AllocationBalance> AllocationBalances { get; set; }
        public virtual ICollection<MovementBalance> MovementBalances { get; set; }
        public virtual ICollection<TraceableItemArch> TraceableItemArches { get; set; }
        public virtual ICollection<TraceableItem> TraceableItems { get; set; }
        public virtual ICollection<TransactionArchive> TransactionArchives { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}