// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class View_1
    {
        public long? ForecastID { get; set; }
        public long? SLCustomerAccountID { get; set; }
        public short? Year { get; set; }
        public byte? Week { get; set; }
        public DateTime? WeekStartDate { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Adjustments { get; set; }
        public decimal? Actual { get; set; }
        public string CustomerAccountName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}