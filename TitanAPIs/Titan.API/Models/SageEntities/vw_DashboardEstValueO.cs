// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_DashboardEstValueO
    {
        public long EstID { get; set; }
        public string EstimateNumber { get; set; }
        public string Description { get; set; }
        public string AccountRef { get; set; }
        public string AccountName { get; set; }
        public decimal TotalSelling { get; set; }
        public decimal? TotalProfit { get; set; }
        public DateTime? DateEntered { get; set; }
    }
}