// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SABRE_BC_Production_Margin
    {
        public string CustomerAccountName { get; set; }
        public string Code { get; set; }
        public decimal? Turnover { get; set; }
        public decimal? Despatch_Qty { get; set; }
        public int? Despatched_Month { get; set; }
        public int? Despatched_Year { get; set; }
        public decimal? Material_Cost_of_Sales { get; set; }
        public decimal? Operations_Labour_Cost { get; set; }
        public decimal? Gross_Margin { get; set; }
    }
}