// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class ProductGroupBatchAttribute
    {
        public long ProductGroupBatchAttributeID { get; set; }
        public long ProductGroupID { get; set; }
        public string Title { get; set; }
        public string DefaultValue { get; set; }
        public bool IsCompulsory { get; set; }
        public bool? IsInUse { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
    }
}