﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepMaximumBuild
    {
        public int ID { get; set; }
        public string BomReference { get; set; }
        public string BomDescription { get; set; }
        public decimal? MaxBuildQty { get; set; }
        public decimal? BomFreeStock { get; set; }
        public decimal? PossibleStock { get; set; }
        public string Sequence { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal? QtyOnBom { get; set; }
        public decimal? FreeStock { get; set; }
        public decimal CanMake { get; set; }
        public bool SubAssembly { get; set; }
        public bool Phantom { get; set; }
        public int SessionID { get; set; }
        public string NonStockItem { get; set; }
    }
}