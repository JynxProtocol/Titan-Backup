// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SLAllocHeaderPostedEntryVw
    {
        public long SLAllocationHeaderID { get; set; }
        public DateTime AllocationDate { get; set; }
        public int UserNumber { get; set; }
        public string UserName { get; set; }
        public long SLCustomerAccountID { get; set; }
        public long UniqueReferenceNumber { get; set; }
        public long SLAllocationTypeID { get; set; }
        public bool IsComplete { get; set; }
        public long SLPostedCustomerTranID { get; set; }
        public string QueryCode { get; set; }
        public decimal? TotalDebitAllocation { get; set; }
    }
}