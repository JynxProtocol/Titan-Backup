﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSMerchantAccount
    {
        public long SYSMerchantAccountID { get; set; }
        public string Description { get; set; }
        public long? CBAccountID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountCostCentre { get; set; }
        public string AccountDepartment { get; set; }

        public virtual CBAccount CBAccount { get; set; }
    }
}