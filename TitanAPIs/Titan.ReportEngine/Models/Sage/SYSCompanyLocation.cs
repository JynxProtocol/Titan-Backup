﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSCompanyLocation
    {
        public long SYSCompanyLocationID { get; set; }
        public long SYSCompanyID { get; set; }
        public long SYSCountryCodeID { get; set; }
        public long SYSCompanyLocationTypeID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public string WebAddress { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSCompany SYSCompany { get; set; }
        public virtual SYSCountryCode SYSCountryCode { get; set; }
    }
}