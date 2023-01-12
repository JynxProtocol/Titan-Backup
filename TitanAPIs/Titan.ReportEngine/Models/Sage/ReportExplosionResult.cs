﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class ReportExplosionResult
    {
        public long ReportExplosionResultID { get; set; }
        public long ReportSessionID { get; set; }
        public int HierarchyLevel { get; set; }
        public long BomComponentLineID { get; set; }
        public string IndentedReference { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public long? BomRecordID { get; set; }
        public string LineType { get; set; }
        public string BomVersion { get; set; }
        public string BomVersionStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ReportSession ReportSession { get; set; }
    }
}