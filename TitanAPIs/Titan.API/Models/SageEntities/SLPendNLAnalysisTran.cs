﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLPendNLAnalysisTran
    {
        public long SLPendNLAnalysisTranID { get; set; }
        public long SLPendCustomerTranID { get; set; }
        public decimal TransactionValue { get; set; }
        public string NominalAccountNumber { get; set; }
        public string NominalAccountCostCentre { get; set; }
        public string NominalAccountDepartment { get; set; }
        public string NominalAnalysisNarrative { get; set; }
        public string TransactionAnalysisCode { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}