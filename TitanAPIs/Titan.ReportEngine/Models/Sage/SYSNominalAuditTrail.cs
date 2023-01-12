﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSNominalAuditTrail
    {
        public long SYSNominalAuditTrailID { get; set; }
        public long SYSAuditTrailID { get; set; }
        public string AccountNumber { get; set; }
        public string CostCentre { get; set; }
        public string Department { get; set; }
        public string AccountName { get; set; }
        public bool NominalExistedAtPostTime { get; set; }
        public decimal NominalValue { get; set; }
        public string Narrative { get; set; }
        public string Reference { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? PostedDate { get; set; }
        public int UserNumber { get; set; }
        public long Source { get; set; }
        public long UniqueReferenceNumber { get; set; }
        public string TradingNominalAnalysisCode { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSAuditTrail SYSAuditTrail { get; set; }
    }
}