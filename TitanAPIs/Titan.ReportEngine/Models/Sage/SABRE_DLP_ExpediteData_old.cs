﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SABRE_DLP_ExpediteData_old
    {
        public string SupplierAccountNumber { get; set; }
        public string SupplierAccountName { get; set; }
        public string ContactName { get; set; }
        public string DocumentNo { get; set; }
        public decimal? OnOrderQuantity { get; set; }
        public decimal? ReceiptReturnQuantity { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string ContactValue { get; set; }
        public long? AuthorisationStatusID { get; set; }
    }
}