﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class CBPaymentCloudProvider
    {
        public CBPaymentCloudProvider()
        {
            CBPaymentCloudProviderBanks = new HashSet<CBPaymentCloudProviderBank>();
        }

        public long CBPaymentCloudProviderID { get; set; }
        public string ServiceProviderKey { get; set; }
        public string ServiceProviderName { get; set; }
        public string ServiceProviderAccountKey { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<CBPaymentCloudProviderBank> CBPaymentCloudProviderBanks { get; set; }
    }
}