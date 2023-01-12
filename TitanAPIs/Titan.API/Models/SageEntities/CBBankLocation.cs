﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CBBankLocation
    {
        public long CBBankLocationID { get; set; }
        public long CBAccountID { get; set; }
        public long CBAccountLocationTypeID { get; set; }
        public string ContactName { get; set; }
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
        public DateTime DateTimeUpdated { get; set; }

        public virtual CBAccount CBAccount { get; set; }
        public virtual CBAccountLocationType CBAccountLocationType { get; set; }
    }
}