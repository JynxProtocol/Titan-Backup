// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepJobAllocIssue
    {
        public long ID { get; set; }
        public long? StageID { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal? Required { get; set; }
        public decimal? Allocated { get; set; }
        public decimal? Issued { get; set; }
        public decimal? Scrapped { get; set; }
        public string ReasonForScrap { get; set; }
        public long? MFGAllocationLineID { get; set; }
    }
}