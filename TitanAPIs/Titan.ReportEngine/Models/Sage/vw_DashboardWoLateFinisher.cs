// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class vw_DashboardWoLateFinisher
    {
        public long? WoID { get; set; }
        public string WorksOrderNumber { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal QtyRequired { get; set; }
    }
}