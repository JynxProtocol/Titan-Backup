﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class Sabre_DLP_SupplierLate
    {
        public string SupplierAccountName { get; set; }
        public string DocumentNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ReceiptReturnDate { get; set; }
        public int? Days_Late { get; set; }
        public int? Delivery_Month { get; set; }
        public int? OrderYear { get; set; }
        public DateTime? DocumentDate { get; set; }
    }
}