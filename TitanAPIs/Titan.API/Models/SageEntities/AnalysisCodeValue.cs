// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class AnalysisCodeValue
    {
        public long AnalysisCodeValueID { get; set; }
        public long AnalysisCodeID { get; set; }
        public string Value { get; set; }
        public bool IsDefault { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual AnalysisCode AnalysisCode { get; set; }
    }
}