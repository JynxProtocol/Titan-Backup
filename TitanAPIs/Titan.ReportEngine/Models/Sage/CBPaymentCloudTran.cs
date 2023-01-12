﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class CBPaymentCloudTran
    {
        public long CBPaymentCloudTranID { get; set; }
        public string PaymentID { get; set; }
        public string PaymentSubmissionID { get; set; }
        public string ServiceProviderAccountKey { get; set; }
        public string ServiceProviderName { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountName { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Fee { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ConversionRate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public long CBPaymentCloudTranStatusTypeID { get; set; }
        public long CBPaymentCloudTranTypeID { get; set; }
        public string RelatedPaymentSubmissionID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual CBPaymentCloudTranStatusType CBPaymentCloudTranStatusType { get; set; }
        public virtual CBPaymentCloudTranType CBPaymentCloudTranType { get; set; }
    }
}