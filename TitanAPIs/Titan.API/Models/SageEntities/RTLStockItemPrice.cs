﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class RTLStockItemPrice
    {
        public long StockItemPriceID { get; set; }
        public long PrdHierNodeId { get; set; }
        public long PriceBandID { get; set; }
        public decimal Price { get; set; }
        public bool UseStandard { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}