﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class vw_RepSalesForecastHeader
    {
        public long ID { get; set; }
        public string Reference { get; set; }
        public DateTime? ForecastStart { get; set; }
        public DateTime? ForecastEnd { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Status { get; set; }
        public string FinancialYearBasis { get; set; }
        public long WarehouseID { get; set; }
    }
}