﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class ReportBomComparisonResult
    {
        public long ReportBomComparisonResultID { get; set; }
        public long ReportBomComparisonItemID { get; set; }
        public long ReportSessionID { get; set; }
        public string Reference1 { get; set; }
        public string Description1 { get; set; }
        public string Quantity1 { get; set; }
        public string UnitOfMeasure1 { get; set; }
        public long DbKey1 { get; set; }
        public string Reference2 { get; set; }
        public string Description2 { get; set; }
        public string Quantity2 { get; set; }
        public string UnitOfMeasure2 { get; set; }
        public long DbKey2 { get; set; }
        public string Difference { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ReportBomComparisonItem ReportBomComparisonItem { get; set; }
        public virtual ReportSession ReportSession { get; set; }
    }
}