﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BomBuildProduct
    {
        public long BomBuildProductID { get; set; }
        public long BomRecordID { get; set; }
        public string StockCode { get; set; }
        public string StockDescription { get; set; }
        public long MseStockItemID { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BomRecord BomRecord { get; set; }
        public virtual MseStockItem MseStockItem { get; set; }
    }
}