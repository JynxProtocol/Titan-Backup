﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class ConformitySalesOrderItem
    {
        public long ID { get; set; }
        public long? HeaderID { get; set; }
        public long? SONumber { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal LineTotal { get; set; }
        public string Comment_1 { get; set; }
        public string Comment_2 { get; set; }
        public string Units { get; set; }

        public virtual Conformity Header { get; set; }
    }
}