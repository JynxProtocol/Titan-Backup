﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class StocktakeSelector
    {
        public StocktakeSelector()
        {
            Stocktakes = new HashSet<Stocktake>();
        }

        public long StocktakeSelectorID { get; set; }
        public string StocktakeSelectorName { get; set; }

        public virtual ICollection<Stocktake> Stocktakes { get; set; }
    }
}