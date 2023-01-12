﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PlanMakeForStock
    {
        public long ID { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal QtyCompleted { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public long WarehouseID { get; set; }
        public decimal? QtyOutstanding { get; set; }
        public bool ExcludeFromReplenishment { get; set; }
        public string Reference { get; set; }
    }
}