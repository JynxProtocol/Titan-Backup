﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class StocktakeType
    {
        public StocktakeType()
        {
            Stocktakes = new HashSet<Stocktake>();
        }

        public long StocktakeTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Stocktake> Stocktakes { get; set; }
    }
}