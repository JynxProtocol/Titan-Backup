// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PCCostItemAnalysisField
    {
        public long PCCostItemAnalysisFieldID { get; set; }
        public long PCCostItemID { get; set; }
        public long PCAnalysisFieldID { get; set; }
        public long? PCAnalysisFieldValueID { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual PCCostItem PCCostItem { get; set; }
    }
}