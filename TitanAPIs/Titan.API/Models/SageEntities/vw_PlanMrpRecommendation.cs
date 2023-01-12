﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_PlanMrpRecommendation
    {
        public long ID { get; set; }
        public string RecNumber { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal DemandQuantity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DueLessSafety { get; set; }
        public decimal Cost { get; set; }
        public decimal PhysicalStock { get; set; }
        public decimal DueInQty { get; set; }
        public decimal NetFree { get; set; }
        public decimal Rec1 { get; set; }
        public decimal Rec2 { get; set; }
        public string Message { get; set; }
        public string BuyerCode { get; set; }
        public string Supplier { get; set; }
        public string PlanPeriod { get; set; }
        public string Status { get; set; }
        public bool FollowUp { get; set; }
        public string CreatedReference { get; set; }
        public string OrderDetails { get; set; }
        public bool SubContract { get; set; }
        public bool ExcludeFromProjection { get; set; }
        public long Category { get; set; }
        public int Priority { get; set; }
        public string Linked { get; set; }
        public long? WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public string ParentTag { get; set; }
        public string DestinationItem { get; set; }
        public DateTime? NeededByDate { get; set; }
        public bool? Late { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}