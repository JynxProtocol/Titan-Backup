﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class INCREASE_Despatch_View_OLD_20170213
    {
        public string Product_Group { get; set; }
        public string Cust_Acc { get; set; }
        public string Cust_Name { get; set; }
        public string SO_Number { get; set; }
        public DateTime? SO_Date { get; set; }
        public short Line_Number { get; set; }
        public string Stock_Code { get; set; }
        public string Stock_Description { get; set; }
        public decimal Line_Qty { get; set; }
        public DateTime? Requested_Del_Date { get; set; }
        public DateTime? Promised_Date { get; set; }
        public string GRN_Number { get; set; }
        public decimal Desp_Quantity { get; set; }
        public DateTime Desp_Date { get; set; }
        public string WorksOrderNumber { get; set; }
        public string Date_Reps_Recv_d { get; set; }
    }
}