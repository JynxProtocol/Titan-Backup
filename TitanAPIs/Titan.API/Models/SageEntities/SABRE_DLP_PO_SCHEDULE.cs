﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SABRE_DLP_PO_SCHEDULE
    {
        public string DocumentNo { get; set; }
        public string SupplierAccountName { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public decimal LineQuantity { get; set; }
        public decimal ReceiptReturnQuantity { get; set; }
        public decimal? QtyOutstanding { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DocumentCreatedBy { get; set; }
        public DateTime? Date_Raised { get; set; }
    }
}