﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLFinanceCharge
    {
        public SLFinanceCharge()
        {
            SLCustomerAccounts = new HashSet<SLCustomerAccount>();
        }

        public long SLFinanceChargeID { get; set; }
        public short FinanceChargeCode { get; set; }
        public decimal MonthlyPercentageCharge { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<SLCustomerAccount> SLCustomerAccounts { get; set; }
    }
}