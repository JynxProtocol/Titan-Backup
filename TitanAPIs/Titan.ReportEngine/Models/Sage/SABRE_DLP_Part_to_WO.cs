// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SABRE_DLP_Part_to_WO
    {
        public string WorksOrderNumber { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal Required { get; set; }
        public decimal Allocated { get; set; }
        public decimal Issued { get; set; }
        public string BomReference { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}