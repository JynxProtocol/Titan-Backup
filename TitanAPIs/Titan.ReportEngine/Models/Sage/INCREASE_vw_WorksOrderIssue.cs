﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class INCREASE_vw_WorksOrderIssue
    {
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountName { get; set; }
        public string SOP_Order_No { get; set; }
        public short PrintSequenceNumber { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public decimal Order_Line_Qty { get; set; }
        public string SO_Status { get; set; }
        public string Allocation_WO_Link { get; set; }
        public decimal? AllocatedQuantity { get; set; }
        public string DespatchNo { get; set; }
        public DateTime? DespatchReceiptDate { get; set; }
        public string Despatch_WO_Link { get; set; }
        public decimal? DespatchReceiptQuantity { get; set; }
        public DateTime? InvoiceCreditDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Invoice_WO_Link { get; set; }
        public decimal? InvoiceCreditQuantity { get; set; }
        public string DestinationItem { get; set; }
    }
}