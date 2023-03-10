// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SagePaymentsSupplierStatus
    {
        public SagePaymentsSupplierStatus()
        {
            NLSettings = new HashSet<NLSetting>();
            PLSupplierAccounts = new HashSet<PLSupplierAccount>();
        }

        public long SagePaymentsSupplierStatusID { get; set; }
        public string Name { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<NLSetting> NLSettings { get; set; }
        public virtual ICollection<PLSupplierAccount> PLSupplierAccounts { get; set; }
    }
}