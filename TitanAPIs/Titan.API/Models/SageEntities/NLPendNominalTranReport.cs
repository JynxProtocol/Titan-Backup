﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLPendNominalTranReport
    {
        public long NLPendNominalTranReportID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountCostCentre { get; set; }
        public string AccountDepartment { get; set; }
        public string PostingReferenceFormatStringDashes { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Reference { get; set; }
        public string Narrative { get; set; }
        public decimal DebitValue { get; set; }
        public decimal CreditValue { get; set; }
    }
}