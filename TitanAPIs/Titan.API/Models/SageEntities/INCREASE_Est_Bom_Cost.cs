﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class INCREASE_Est_Bom_Cost
    {
        public string BomReference { get; set; }
        public string BomVersion { get; set; }
        public decimal QtyCosted { get; set; }
        public decimal? Labour_Cost { get; set; }
        public decimal? Machine_Cost { get; set; }
        public decimal? Materials_Cost { get; set; }
        public decimal? Subcontract_Cost { get; set; }
        public decimal? Tooling_Cost { get; set; }
        public decimal? Total_Est_Cost { get; set; }
    }
}