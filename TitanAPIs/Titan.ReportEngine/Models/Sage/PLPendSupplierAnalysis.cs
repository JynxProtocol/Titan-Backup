﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PLPendSupplierAnalysis
    {
        public long PLPendSupplierAnalysisID { get; set; }
        public long PLPendSupplierAccountID { get; set; }
        public string AnalysisName { get; set; }
        public string AnalysisValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual PLPendSupplierAccount PLPendSupplierAccount { get; set; }
    }
}