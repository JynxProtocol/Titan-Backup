﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SABRE_DLP_LastCost
    {
        public DateTime? Last_Received_Date { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Last_Cost { get; set; }
        public string SupplierAccountName { get; set; }
        public short LeadTime { get; set; }
        public decimal LastCostPrice { get; set; }
    }
}