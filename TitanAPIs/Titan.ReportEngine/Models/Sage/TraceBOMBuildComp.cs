// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TraceBOMBuildComp
    {
        public long TraceBOMBuildCompID { get; set; }
        public long TraceBOMBuildFinItemID { get; set; }
        public long BOMBuildComponentID { get; set; }
        public long TraceableBinItemID { get; set; }
        public decimal Quantity { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BOMBuildComponent BOMBuildComponent { get; set; }
        public virtual TraceBOMBuildFinItem TraceBOMBuildFinItem { get; set; }
    }
}