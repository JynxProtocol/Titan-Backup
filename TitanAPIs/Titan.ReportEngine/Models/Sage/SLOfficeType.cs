﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SLOfficeType
    {
        public SLOfficeType()
        {
            SLCustomerAccounts = new HashSet<SLCustomerAccount>();
            SLPendCustomerAccounts = new HashSet<SLPendCustomerAccount>();
        }

        public long SLOfficeTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SLCustomerAccount> SLCustomerAccounts { get; set; }
        public virtual ICollection<SLPendCustomerAccount> SLPendCustomerAccounts { get; set; }
    }
}