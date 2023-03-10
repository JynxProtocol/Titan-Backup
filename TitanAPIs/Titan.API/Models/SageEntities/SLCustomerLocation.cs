// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLCustomerLocation
    {
        public SLCustomerLocation()
        {
            SLCustomerContacts = new HashSet<SLCustomerContact>();
            SLCustomerDocuments = new HashSet<SLCustomerDocument>();
        }

        public long SLCustomerLocationID { get; set; }
        public long SLCustomerAccountID { get; set; }
        public long SYSCountryCodeID { get; set; }
        public long SYSTraderLocationTypeID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public long AddressCountryID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSCountryCode AddressCountry { get; set; }
        public virtual SLCustomerAccount SLCustomerAccount { get; set; }
        public virtual SYSCountryCode SYSCountryCode { get; set; }
        public virtual SYSTraderLocationType SYSTraderLocationType { get; set; }
        public virtual ICollection<SLCustomerContact> SLCustomerContacts { get; set; }
        public virtual ICollection<SLCustomerDocument> SLCustomerDocuments { get; set; }
    }
}