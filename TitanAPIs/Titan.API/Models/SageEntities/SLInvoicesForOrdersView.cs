﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLInvoicesForOrdersView
    {
        public string mmsslin_OrderNumber { get; set; }
        public string mmsslin_InvoiceNo { get; set; }
        public DateTime mmsslin_InvoiceDate { get; set; }
        public double? mmsslin_InvoiceValue { get; set; }
        public string mmsslin_DocumentStatus { get; set; }
        public long mmsslin_OrderReturnID { get; set; }
    }
}