﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PayPortGroupDiscVw
    {
        public long? CustomerDiscountGroupID { get; set; }
        public long? PriceBandID { get; set; }
        public long ProductGroupDiscountID { get; set; }
        public string DiscGrpName { get; set; }
        public string PriceBandName { get; set; }
        public string Category { get; set; }
        public long ProductGroupID { get; set; }
        public decimal QuantityMoreThan { get; set; }
        public decimal DiscountPercentValue { get; set; }
        public string ItemCode { get; set; }
    }
}