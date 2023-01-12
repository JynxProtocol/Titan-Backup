﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class vw_DashboardPlanDue14DaysExcludedMp
    {
        public long ID { get; set; }
        public string Type { get; set; }
        public decimal UniqueIdentifier { get; set; }
        public long StageID { get; set; }
        public string Reference { get; set; }
        public string AccRef { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public long? Category { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DateAdded { get; set; }
        public string AddedBy { get; set; }
        public bool Excluded { get; set; }
        public string ExclusionReason { get; set; }
        public string CustomerAnalysis_1 { get; set; }
        public string CustomerAnalysis_2 { get; set; }
        public string CustomerAnalysis_3 { get; set; }
        public byte OrderType { get; set; }
        public string Status { get; set; }
        public bool ReadInProgress { get; set; }
        public string Comment_1 { get; set; }
        public string Comment_2 { get; set; }
        public string CustOrderNumber { get; set; }
        public int Linked { get; set; }
        public long WarehouseID { get; set; }
        public bool ManualExclusion { get; set; }
        public bool OnHold { get; set; }
        public bool CustomerOnHold { get; set; }
        public int OrderFulfilmentMethod { get; set; }
        public int BackToBackStatus { get; set; }
        public long Header { get; set; }
        public string Period { get; set; }
        public string WarehouseName { get; set; }
    }
}