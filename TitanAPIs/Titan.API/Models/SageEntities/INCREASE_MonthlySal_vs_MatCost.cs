﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class INCREASE_MonthlySal_vs_MatCost
    {
        public string Product_Group { get; set; }
        public decimal? Sales_Price { get; set; }
        public decimal? Material_Costs { get; set; }
        public int? Invoice_Month { get; set; }
        public int? Invoice_Year { get; set; }
    }
}