// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSCompany
    {
        public SYSCompany()
        {
            SYSCompanyLocations = new HashSet<SYSCompanyLocation>();
        }

        public long SYSCompanyID { get; set; }
        public string CompanyName { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public string SagePaymentsDatasetIdentifier { get; set; }
        public DateTime? SagePaymentsLastSyncDateTime { get; set; }
        public string SagePaymentsTag { get; set; }
        public long? EDULocalEducationAuthorityID { get; set; }
        public long EDUSchoolTypeID { get; set; }
        public bool IsAcademy { get; set; }
        public string BankCloudCompanyID { get; set; }
        public string PaymentCloudCompanyID { get; set; }
        public string EORINumber { get; set; }

        public virtual EDULocalEducationAuthority EDULocalEducationAuthority { get; set; }
        public virtual EDUSchoolType EDUSchoolType { get; set; }
        public virtual ICollection<SYSCompanyLocation> SYSCompanyLocations { get; set; }
    }
}