// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SABRE_DLP_Dry_prep_plan_2015
    {
        public string Product_Group { get; set; }
        public string Acc_Name { get; set; }
        public string Sales_Order_Number { get; set; }
        public string WorksOrderNumber { get; set; }
        public string CustOrderNumber { get; set; }
        public string PickingListComment { get; set; }
        public string ProjectNumber { get; set; }
        public string Works_Order_Status { get; set; }
        public DateTime? WO_Completed_Date { get; set; }
        public string Next_Operation { get; set; }
        public string Item_Code { get; set; }
        public string Description { get; set; }
        public string SO_Status { get; set; }
        public decimal Sales_Qty { get; set; }
        public decimal? Qty_On_Site { get; set; }
        public DateTime? Date_Reps_Rec_d_ { get; set; }
        public DateTime? Promised_Despatch_Date { get; set; }
        public DateTime? Production_Start_Date { get; set; }
        public string Disruption_Code { get; set; }
        public decimal? D { get; set; }
        public decimal? C { get; set; }
        public decimal? I { get; set; }
        public decimal? CT { get; set; }
        public decimal? B { get; set; }
        public decimal? T { get; set; }
        public decimal? PP { get; set; }
        public decimal? P { get; set; }
        public decimal Pack { get; set; }
        public decimal Del { get; set; }
        public string Comments { get; set; }
        public string GRN { get; set; }
        public bool Printed { get; set; }
        public DateTime? EndDate { get; set; }
    }
}