// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class Salutation
    {
        public Salutation()
        {
            EstimateContactSalutations = new HashSet<Estimate>();
            EstimateSalespersonSalutations = new HashSet<Estimate>();
            JobContactSalutations = new HashSet<Job>();
            JobSalesInvoiceHeaders = new HashSet<JobSalesInvoiceHeader>();
            JobSalespersonSalutations = new HashSet<Job>();
            MFGContacts = new HashSet<MFGContact>();
            PLSupplierContacts = new HashSet<PLSupplierContact>();
            PlanMrpRecommendations = new HashSet<PlanMrpRecommendation>();
            ProspectAccountManagerSalutations = new HashSet<Prospect>();
            ProspectContactSalutations = new HashSet<Prospect>();
            ProspectDelContactSalutations = new HashSet<Prospect>();
            ProspectSalesRepSalutations = new HashSet<Prospect>();
            ProspectTradeContactSalutations = new HashSet<Prospect>();
            SLCustomerContacts = new HashSet<SLCustomerContact>();
            SubContractingDespatches = new HashSet<SubContractingDespatch>();
            WorksOrders = new HashSet<WorksOrder>();
        }

        public long SalutationID { get; set; }
        public bool IsDefault { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long SequenceNumber { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<Estimate> EstimateContactSalutations { get; set; }
        public virtual ICollection<Estimate> EstimateSalespersonSalutations { get; set; }
        public virtual ICollection<Job> JobContactSalutations { get; set; }
        public virtual ICollection<JobSalesInvoiceHeader> JobSalesInvoiceHeaders { get; set; }
        public virtual ICollection<Job> JobSalespersonSalutations { get; set; }
        public virtual ICollection<MFGContact> MFGContacts { get; set; }
        public virtual ICollection<PLSupplierContact> PLSupplierContacts { get; set; }
        public virtual ICollection<PlanMrpRecommendation> PlanMrpRecommendations { get; set; }
        public virtual ICollection<Prospect> ProspectAccountManagerSalutations { get; set; }
        public virtual ICollection<Prospect> ProspectContactSalutations { get; set; }
        public virtual ICollection<Prospect> ProspectDelContactSalutations { get; set; }
        public virtual ICollection<Prospect> ProspectSalesRepSalutations { get; set; }
        public virtual ICollection<Prospect> ProspectTradeContactSalutations { get; set; }
        public virtual ICollection<SLCustomerContact> SLCustomerContacts { get; set; }
        public virtual ICollection<SubContractingDespatch> SubContractingDespatches { get; set; }
        public virtual ICollection<WorksOrder> WorksOrders { get; set; }
    }
}