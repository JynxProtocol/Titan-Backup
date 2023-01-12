﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSPaymentTermsBasis
    {
        public SYSPaymentTermsBasis()
        {
            PLPendSupplierAccounts = new HashSet<PLPendSupplierAccount>();
            PLSettings = new HashSet<PLSetting>();
            PLSupplierAccounts = new HashSet<PLSupplierAccount>();
            SLCustomerAccounts = new HashSet<SLCustomerAccount>();
            SLPendCustomerAccounts = new HashSet<SLPendCustomerAccount>();
            SLSettings = new HashSet<SLSetting>();
        }

        public long SYSPaymentTermsBasisID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PLPendSupplierAccount> PLPendSupplierAccounts { get; set; }
        public virtual ICollection<PLSetting> PLSettings { get; set; }
        public virtual ICollection<PLSupplierAccount> PLSupplierAccounts { get; set; }
        public virtual ICollection<SLCustomerAccount> SLCustomerAccounts { get; set; }
        public virtual ICollection<SLPendCustomerAccount> SLPendCustomerAccounts { get; set; }
        public virtual ICollection<SLSetting> SLSettings { get; set; }
    }
}