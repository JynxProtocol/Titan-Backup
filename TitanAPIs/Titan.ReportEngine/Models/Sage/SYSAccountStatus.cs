﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSAccountStatus
    {
        public SYSAccountStatus()
        {
            CBAccounts = new HashSet<CBAccount>();
            NLNominalAccounts = new HashSet<NLNominalAccount>();
            NLPendNominalAccounts = new HashSet<NLPendNominalAccount>();
            NLReportCategoryBudgets = new HashSet<NLReportCategoryBudget>();
            PLPendSupplierAccounts = new HashSet<PLPendSupplierAccount>();
            PLSupplierAccounts = new HashSet<PLSupplierAccount>();
            SLCustomerAccounts = new HashSet<SLCustomerAccount>();
            SLPendCustomerAccounts = new HashSet<SLPendCustomerAccount>();
        }

        public long SYSAccountStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CBAccount> CBAccounts { get; set; }
        public virtual ICollection<NLNominalAccount> NLNominalAccounts { get; set; }
        public virtual ICollection<NLPendNominalAccount> NLPendNominalAccounts { get; set; }
        public virtual ICollection<NLReportCategoryBudget> NLReportCategoryBudgets { get; set; }
        public virtual ICollection<PLPendSupplierAccount> PLPendSupplierAccounts { get; set; }
        public virtual ICollection<PLSupplierAccount> PLSupplierAccounts { get; set; }
        public virtual ICollection<SLCustomerAccount> SLCustomerAccounts { get; set; }
        public virtual ICollection<SLPendCustomerAccount> SLPendCustomerAccounts { get; set; }
    }
}