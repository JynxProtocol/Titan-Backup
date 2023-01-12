﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSHumanResource
    {
        public TSHumanResource()
        {
            PCProjectEntries = new HashSet<PCProjectEntry>();
            PCProjectResourceLinks = new HashSet<PCProjectResourceLink>();
            TSHumanResourceChargeRateLinks = new HashSet<TSHumanResourceChargeRateLink>();
            TSHumanResourceCostRateLinks = new HashSet<TSHumanResourceCostRateLink>();
            TSHumanResourcePayRateLinks = new HashSet<TSHumanResourcePayRateLink>();
            TSResourceResourceLinkOwners = new HashSet<TSResourceResourceLink>();
            TSResourceResourceLinkResources = new HashSet<TSResourceResourceLink>();
        }

        public long TSHumanResourceID { get; set; }
        public long TSPersonID { get; set; }
        public string EmployeeReference { get; set; }
        public long TSPaymentFrequencyTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? PLSupplierAccountID { get; set; }
        public string WorksNumber { get; set; }
        public string Description { get; set; }
        public long TSPaymentMethodID { get; set; }
        public long TSResourceStatusTypeID { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankSortCode { get; set; }
        public string BankPaymentReference { get; set; }
        public long StandardTime { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool FilterResources { get; set; }
        public int? MMSUserID { get; set; }
        public long? CBAccountID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual CBAccount CBAccount { get; set; }
        public virtual PLSupplierAccount PLSupplierAccount { get; set; }
        public virtual TSPaymentFrequencyType TSPaymentFrequencyType { get; set; }
        public virtual TSPaymentMethod TSPaymentMethod { get; set; }
        public virtual TSPerson TSPerson { get; set; }
        public virtual TSResourceStatusType TSResourceStatusType { get; set; }
        public virtual ICollection<PCProjectEntry> PCProjectEntries { get; set; }
        public virtual ICollection<PCProjectResourceLink> PCProjectResourceLinks { get; set; }
        public virtual ICollection<TSHumanResourceChargeRateLink> TSHumanResourceChargeRateLinks { get; set; }
        public virtual ICollection<TSHumanResourceCostRateLink> TSHumanResourceCostRateLinks { get; set; }
        public virtual ICollection<TSHumanResourcePayRateLink> TSHumanResourcePayRateLinks { get; set; }
        public virtual ICollection<TSResourceResourceLink> TSResourceResourceLinkOwners { get; set; }
        public virtual ICollection<TSResourceResourceLink> TSResourceResourceLinkResources { get; set; }
    }
}