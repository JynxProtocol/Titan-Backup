﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class Sabre_DLP_SalesSchedule
    {
        public string CustomerAccountName { get; set; }
        public string Sabre_Order_No { get; set; }
        public string Works_Order_No_ { get; set; }
        public string Customer_Document_No_ { get; set; }
        public DateTime? Promised_Delivery_Date { get; set; }
        public string Status { get; set; }
        public string Our_Stock_Code { get; set; }
        public string Description { get; set; }
        public string BR_Cat_No_ { get; set; }
        public string Product_Group { get; set; }
        public decimal? Order_Qty { get; set; }
        public decimal? Outstanding_Qty { get; set; }
        public decimal? On_Site { get; set; }
        public string Despatch_Note_No_ { get; set; }
        public decimal? Despatched { get; set; }
        public DateTime? Despatch_Date { get; set; }
        public string PickingListComment { get; set; }
        public string Date_Reps_Rec_d_ { get; set; }
        public DateTime? Requested_Date { get; set; }
        public long LineTypeID { get; set; }
        public decimal? Qty_Completed { get; set; }
        public string AnalysisCode1 { get; set; }
        public string AnalysisCode2 { get; set; }
        public string AnalysisCode3 { get; set; }
        public decimal UnitSellingPrice { get; set; }
        public long SOPOrderReturnLineID { get; set; }
    }
}