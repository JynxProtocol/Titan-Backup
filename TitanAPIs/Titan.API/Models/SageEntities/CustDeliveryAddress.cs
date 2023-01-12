﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CustDeliveryAddress
    {
        public long CustDeliveryAddressID { get; set; }
        public long CustomerID { get; set; }
        public string Description { get; set; }
        public string PostalName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }
        public string Contact { get; set; }
        public string TelephoneNo { get; set; }
        public string FaxNo { get; set; }
        public string EmailAddress { get; set; }
        public string TaxNo { get; set; }
        public long TaxCodeID { get; set; }
        public long? CountryCodeID { get; set; }
        public bool ThisIsDefaultAddress { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public long? AddressCountryCodeID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSCountryCode AddressCountryCode { get; set; }
        public virtual SYSCountryCode CountryCode { get; set; }
        public virtual SLCustomerAccount Customer { get; set; }
        public virtual SYSTaxRate TaxCode { get; set; }
    }
}