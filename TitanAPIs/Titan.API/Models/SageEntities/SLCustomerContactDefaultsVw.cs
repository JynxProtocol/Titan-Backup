﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLCustomerContactDefaultsVw
    {
        public long SLCustomerAccountID { get; set; }
        public long SLCustomerContactID { get; set; }
        public string Contact { get; set; }
        public bool? IsDefaultRole { get; set; }
        public string DefaultTelephone { get; set; }
        public string DefaultFax { get; set; }
        public string DefaultEmail { get; set; }
        public string DefaultWebsite { get; set; }
        public string DefaultMobile { get; set; }
        public bool? IsPreferredContactForRole { get; set; }
        public string ContactRoleName { get; set; }
    }
}