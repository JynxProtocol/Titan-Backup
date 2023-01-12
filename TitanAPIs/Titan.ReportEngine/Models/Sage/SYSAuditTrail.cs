﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSAuditTrail
    {
        public SYSAuditTrail()
        {
            SYSNominalAuditTrails = new HashSet<SYSNominalAuditTrail>();
            SYSTaxAuditTrails = new HashSet<SYSTaxAuditTrail>();
        }

        public long SYSAuditTrailID { get; set; }
        public bool? AlreadyPrinted { get; set; }
        public bool TaxEntriesExist { get; set; }
        public bool NominalEntriesExist { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public long SYSAuditTrailEntryTypeID { get; set; }
        public string Reference { get; set; }
        public string SecondReference { get; set; }
        public string BatchReference { get; set; }
        public decimal TransactionToBaseExchangeRate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? PostedDate { get; set; }
        public string UserName { get; set; }
        public int UserNumber { get; set; }
        public long Source { get; set; }
        public long UniqueReferenceNumber { get; set; }
        public decimal NetValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal GrossValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSAuditTrailEntryType SYSAuditTrailEntryType { get; set; }
        public virtual ICollection<SYSNominalAuditTrail> SYSNominalAuditTrails { get; set; }
        public virtual ICollection<SYSTaxAuditTrail> SYSTaxAuditTrails { get; set; }
    }
}