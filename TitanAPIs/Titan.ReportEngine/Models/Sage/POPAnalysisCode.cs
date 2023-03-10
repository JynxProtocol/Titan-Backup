// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class POPAnalysisCode
    {
        public long POPAnalysisCodeID { get; set; }
        public bool CanAmend { get; set; }
        public bool IncludeInHistory { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long AnalysisCodeMappingID { get; set; }
        public bool DefaultFromSupplier { get; set; }
        public long? SupplierAnalysisCodeMappingID { get; set; }
        public long? TranHistAnalysisCodeMappingID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual AnalysisCodeMapping AnalysisCodeMapping { get; set; }
        public virtual AnalysisCodeMapping SupplierAnalysisCodeMapping { get; set; }
        public virtual AnalysisCodeMapping TranHistAnalysisCodeMapping { get; set; }
    }
}