﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BOMItemType
    {
        public BOMItemType()
        {
            StockItems = new HashSet<StockItem>();
        }

        public long BOMItemTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}