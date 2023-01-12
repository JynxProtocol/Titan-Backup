﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class Sabre_DLP_Stockreport
    {
        public string Stock_Code { get; set; }
        public string Search { get; set; }
        public string Description { get; set; }
        public string ManPartNumber { get; set; }
        public string Product_Group { get; set; }
        public string Location { get; set; }
        public decimal? Last_Cost { get; set; }
        public decimal Last_Invoice_Price { get; set; }
        public DateTime? Last_Received_Date { get; set; }
        public decimal AverageBuyingPrice { get; set; }
        public DateTime? Last_Issued_Date { get; set; }
        public string Issue_Type { get; set; }
        public decimal? Stock { get; set; }
        public decimal? Allocated { get; set; }
        public decimal? FreeStock { get; set; }
        public decimal? TheoStock { get; set; }
        public short? LeadTime { get; set; }
        public decimal Total_On_Order { get; set; }
        public string Poporders { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal? MinimumOrderQuantity { get; set; }
        public decimal MinimumLevel { get; set; }
        public decimal? _60Month { get; set; }
        public decimal? _36Month { get; set; }
        public decimal? _24Month_Usage { get; set; }
        public decimal? _12Month_Usage { get; set; }
        public string SupplierAccountName { get; set; }
    }
}