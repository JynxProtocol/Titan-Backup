// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class vSalesAllocDateDiscrep
    {
        public decimal AllocatedValue { get; set; }
        public decimal GoodsValueInAccountCurrency { get; set; }
        public DateTime PostedDate { get; set; }
        public long UniqueReferenceNumber { get; set; }
        public string UserName { get; set; }
        public decimal TaxValue { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime AllocationDate { get; set; }
        public long SLPostedCustomerTranID { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountName { get; set; }
        public string TransTypeName { get; set; }
        public string TransTypeShortName { get; set; }
        public string TransactionDateMonth { get; set; }
        public int? TransactionDateMonthNumber { get; set; }
        public int? AllocationDateMonthNumber { get; set; }
        public string AllocationDateMonth { get; set; }
        public int? AllocationDateYear { get; set; }
        public int? TransactionDateYear { get; set; }
    }
}