﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vPurchaseAllocDateDiscrep
    {
        public decimal AllocatedValue { get; set; }
        public decimal GoodsValueInAccountCurrency { get; set; }
        public DateTime PostedDate { get; set; }
        public long UniqueReferenceNumber { get; set; }
        public string UserName { get; set; }
        public decimal TaxValue { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime AllocationDate { get; set; }
        public long PLPostedSupplierTranID { get; set; }
        public string SupplierAccountNumber { get; set; }
        public string SupplierAccountName { get; set; }
        public string TransTypeName { get; set; }
        public string TransTypeShortName { get; set; }
        public int? TransactionDateMonthNumber { get; set; }
        public int? AllocationDateMonthNumber { get; set; }
        public int? AllocationDateYear { get; set; }
        public int? TransactionDateYear { get; set; }
        public string TransactionDateMonth { get; set; }
        public string AllocationDateMonth { get; set; }
    }
}