﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class INCREASE_vw_ProductionPlanMk2
    {
        public long SOPOrderReturnLineID { get; set; }
        public string Type { get; set; }
        public string ProductGroup { get; set; }
        public string SabreOrderNo { get; set; }
        public string OrderStatus { get; set; }
        public string WorksOrderNo { get; set; }
        public string CustomerOrderNo { get; set; }
        public string ProjectNumber { get; set; }
        public string AccNo { get; set; }
        public string AccName { get; set; }
        public string Description { get; set; }
        public string BrCatNo { get; set; }
        public decimal? SalesQty { get; set; }
        public decimal? QtyOnSite { get; set; }
        public DateTime? RepsDueonSite { get; set; }
        public DateTime? PromisedDespatchDate { get; set; }
        public string Status { get; set; }
        public string DateRepsRecd { get; set; }
        public string GRN { get; set; }
        public string BatchNo { get; set; }
        public string PickingListComment { get; set; }
        public DateTime? RequestedDeliveryDate { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public string DismantlingSection { get; set; }
        public string AssemblySection { get; set; }
        public string PaintSection { get; set; }
        public string DeliverySection { get; set; }
        public string ProcessReference { get; set; }
        public decimal? Complete { get; set; }
        public double? Sequence { get; set; }
        public string NextOP { get; set; }
        public string NextOpName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? SeqFilter { get; set; }
    }
}