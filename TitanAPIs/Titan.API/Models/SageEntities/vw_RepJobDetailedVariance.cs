// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepJobDetailedVariance
    {
        public string WorksOrderNumber { get; set; }
        public string JobDescription { get; set; }
        public string ProjectNumber { get; set; }
        public string StageReference { get; set; }
        public string EstimateNumber { get; set; }
        public string EstStageReference { get; set; }
        public long WoID { get; set; }
        public long? JobID { get; set; }
        public long? StageID { get; set; }
        public long? EstID { get; set; }
        public long? EstStageID { get; set; }
        public string RecordType { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal? EstimatedQuantity { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualQuantity { get; set; }
        public decimal? ActualCost { get; set; }
        public decimal? VarianceQuantity { get; set; }
        public decimal? VarianceCost { get; set; }
        public decimal? AllowableQuantity { get; set; }
        public decimal? AllowableCost { get; set; }
    }
}