// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.ReportEngine.Models.Titan
{
    public partial class EQPTracking
    {
        public long ID { get; set; }
        public long HeaderID { get; set; }
        public string WorksOrderNumber { get; set; }
        public string ProcessReference { get; set; }
        public string ProcessDescription { get; set; }
        public decimal InProgress { get; set; }
        public decimal Complete { get; set; }
        public double Sequence { get; set; }
        public string LabourReference { get; set; }
        public string Notes { get; set; }
        public string LabourDescription { get; set; }
        public string LabourNotes { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartedBy { get; set; }
        public string EndedBy { get; set; }
    }
}