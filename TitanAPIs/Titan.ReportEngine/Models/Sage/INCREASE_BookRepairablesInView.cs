﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class INCREASE_BookRepairablesInView
    {
        public string Sabre_Order_No_ { get; set; }
        public string Works_Order_No { get; set; }
        public string Customer_Order_No_ { get; set; }
        public string ProjectNumber { get; set; }
        public string Other_Information { get; set; }
        public string Acc__No { get; set; }
        public string Acc__Name { get; set; }
        public decimal Sales_Qty { get; set; }
        public DateTime? PromisedDeliveryDate { get; set; }
        public string Status { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public long TraceableTypeID { get; set; }
        public decimal Required { get; set; }
        public decimal Allocated { get; set; }
        public decimal Issued { get; set; }
        public long SOPOrderReturnLineID { get; set; }
        public long? AllocID { get; set; }
        public long WorksOrderID { get; set; }
        public short PrintSequenceNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public bool TopLevelOrder { get; set; }
        public long? Locator { get; set; }
        public long ID { get; set; }
        public long ItemID { get; set; }
        public string Code { get; set; }
        public long ProductGroupID { get; set; }
        public bool Printed { get; set; }
    }
}