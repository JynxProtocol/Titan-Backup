// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class RepRequirement
    {
        public long ID { get; set; }
        public int? SessionID { get; set; }
        public string BomReference { get; set; }
        public long? BomID { get; set; }
        public decimal QtyRequired { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal Required { get; set; }
        public decimal Shortage { get; set; }
        public string ItemType { get; set; }
        public string PlanReference { get; set; }
        public long? PlanID { get; set; }
        public string WOrderReference { get; set; }
        public long? WOrderID { get; set; }
        public DateTime? JobStartDate { get; set; }
        public string SuppRef { get; set; }
        public string SuppJobRef { get; set; }
        public decimal Cost { get; set; }
        public int? SubContLeadTime { get; set; }
        public string SubContDetails { get; set; }
        public bool Phantom { get; set; }
        public decimal QtyOnOrder { get; set; }
        public decimal ReorderQty { get; set; }
    }
}