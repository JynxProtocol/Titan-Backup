﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class EstQtyBreak
    {
        public long ID { get; set; }
        public long HeaderID { get; set; }
        public bool Active { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Markup { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal UnitSellingPrice { get; set; }
        public decimal Margin { get; set; }
        public string UnitOfSale { get; set; }
        public string Memo { get; set; }
        public long? StockItemUnitID { get; set; }
        public int AmendType { get; set; }

        public virtual Estimate Header { get; set; }
    }
}