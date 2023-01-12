﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSTaxPeriod
    {
        public SYSTaxPeriod()
        {
            SYSTaxAdjustments = new HashSet<SYSTaxAdjustment>();
            SYSTaxPeriodRateBalances = new HashSet<SYSTaxPeriodRateBalance>();
        }

        public long SYSTaxPeriodID { get; set; }
        public long SYSTaxPeriodStatusTypeID { get; set; }
        public short TaxPeriodNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RunBy { get; set; }
        public DateTime? DateRun { get; set; }
        public decimal LiabilityValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool? OnlineSubmission { get; set; }
        public long? SYSTaxPeriodSubmitStatusID { get; set; }
        public string HMRCReference { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public bool PaidElectronically { get; set; }
        public string PaymentNotification { get; set; }
        public string ExtraInformation { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public string ChargeRefNumber { get; set; }
        public string FormBundleNumber { get; set; }
        public string PaymentIndicator { get; set; }
        public DateTime? ProcessingDateTime { get; set; }
        public string CorrelationIdentifier { get; set; }
        public string ReceiptIdentifier { get; set; }
        public DateTime? PeriodDueDate { get; set; }
        public DateTime? PeriodStartDate { get; set; }
        public DateTime? PeriodEndDate { get; set; }

        public virtual SYSTaxPeriodStatusType SYSTaxPeriodStatusType { get; set; }
        public virtual SYSTaxPeriodSubmitStatus SYSTaxPeriodSubmitStatus { get; set; }
        public virtual ICollection<SYSTaxAdjustment> SYSTaxAdjustments { get; set; }
        public virtual ICollection<SYSTaxPeriodRateBalance> SYSTaxPeriodRateBalances { get; set; }
    }
}