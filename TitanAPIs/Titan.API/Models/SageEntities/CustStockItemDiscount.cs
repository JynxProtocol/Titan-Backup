﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CustStockItemDiscount
    {
        public long CustStockItemDiscountID { get; set; }
        public long CustomerDiscountGroupID { get; set; }
        public long StockItemDiscountID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual CustomerDiscountGroup CustomerDiscountGroup { get; set; }
        public virtual StockItemDiscount StockItemDiscount { get; set; }
    }
}