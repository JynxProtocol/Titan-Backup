﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PCBillingProjectSearchVw
    {
        public long BLBillID { get; set; }
        public string BillNumber { get; set; }
        public string BillType { get; set; }
        public string BillStatus { get; set; }
        public string AccountReference { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalNetValue { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalGrossValue { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal TotalDiscountedNetValue { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public string CreditNo { get; set; }
        public DateTime? CreditDate { get; set; }
        public string CreditStatus { get; set; }
    }
}