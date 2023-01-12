﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepPlanMrpRecommendation
    {
        public long ID { get; set; }
        public string RecNumber { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal? DemandQuantity { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueLessSafety { get; set; }
        public decimal? Cost { get; set; }
        public decimal? PhysicalStock { get; set; }
        public decimal? DueInQty { get; set; }
        public decimal? NetFree { get; set; }
        public decimal? Rec1 { get; set; }
        public decimal? Rec2 { get; set; }
        public string Supplier { get; set; }
        public string Message { get; set; }
        public string BuyerCode { get; set; }
        public string DelAddressName { get; set; }
        public string DelAddress_1 { get; set; }
        public string DelAddress_2 { get; set; }
        public string DelAddress_3 { get; set; }
        public string DelAddress_4 { get; set; }
        public string DelAddress_5 { get; set; }
        public long StageID { get; set; }
        public long BomID { get; set; }
        public string Exploded { get; set; }
        public long PoLineNumber { get; set; }
        public string PONumber { get; set; }
        public string Status { get; set; }
        public long SplitID { get; set; }
        public short EstLeadTime { get; set; }
        public string FollowUp { get; set; }
        public long WoID { get; set; }
        public string Period { get; set; }
        public string CreatedReference { get; set; }
        public string OrderDetails { get; set; }
        public string SubContract { get; set; }
        public string StartDateWithTime { get; set; }
        public string EndDateWithTime { get; set; }
        public bool ExcludeFromProjection { get; set; }
        public bool ExcludeFromPlanner { get; set; }
        public DateTime? NeededByDate { get; set; }
        public string Late { get; set; }
        public long Category { get; set; }
        public int Priority { get; set; }
        public string Linked { get; set; }
        public long? WarehouseID { get; set; }
        public string ParentTag { get; set; }
        public string DelAddressLine1 { get; set; }
        public string DelAddressLine2 { get; set; }
        public string DelAddressLine3 { get; set; }
        public string DelAddressLine4 { get; set; }
        public string DelAddressPostcode { get; set; }
        public string DelAddressCity { get; set; }
        public string DelAddressCounty { get; set; }
        public long? DelAddressCountryID { get; set; }
        public string DelAddressCountryName { get; set; }
        public long BuyerCodeSalutationID { get; set; }
        public string BuyerCodeFirstName { get; set; }
        public string BuyerCodeMiddleName { get; set; }
        public string BuyerCodeLastName { get; set; }
        public string BuyerCodePreMigratedData { get; set; }
        public string BuyerCodeSalutation { get; set; }
        public string CondensedDelAddress { get; set; }
    }
}