﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SABRE_DLP_UnsatisfiedLine
    {
        public string DocumentNo { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public decimal LineQuantity { get; set; }
        public decimal ReceiptReturnQuantity { get; set; }
        public long DocumentStatusID { get; set; }
        public string SupplierAccountName { get; set; }
        public DateTime? DocumentDate { get; set; }
    }
}