﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SupplierReorderLevel
    {
        public string cn_ref { get; set; }
        public double? cs_reordq { get; set; }
        public string New_Supp_Code { get; set; }
        public double? Preferred_Supplier_Flag { get; set; }
        public string Supplier_Part_Code { get; set; }
        public double? Supplier_Lead_Time { get; set; }
        public double? Lead_Time_Unit { get; set; }
        public double? Corrected_Last_Cost_Price { get; set; }
        public string Pricing_Source { get; set; }
        public string List_Price_Exp_Date { get; set; }
        public double? Sense_Check_Cost_Price { get; set; }
    }
}