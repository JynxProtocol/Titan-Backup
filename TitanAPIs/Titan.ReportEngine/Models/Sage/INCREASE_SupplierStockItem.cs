﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class INCREASE_SupplierStockItem
    {
        public string supplier { get; set; }
        public string stock_code { get; set; }
        public string supp_stk_code { get; set; }
        public double? forcurrvaluedecpl { get; set; }
        public string currency { get; set; }
        public double? cost_price { get; set; }
        public double? last_del_qty { get; set; }
        public double? last_unit_cost { get; set; }
        public DateTime? last_del_date { get; set; }
        public double? min_order_qty { get; set; }
        public double? lead_time_day { get; set; }
        public string pref_supp { get; set; }
        public DateTime? sq_crdate { get; set; }
        public double? foreign_unit_dec_places { get; set; }
        public string stk_profile { get; set; }
    }
}