﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLAccountNumberCostCentre
    {
        public NLAccountNumberCostCentre()
        {
            NLNominalAccounts = new HashSet<NLNominalAccount>();
        }

        public long NLAccountNumberCostCentreID { get; set; }
        public long NLAccountNumberID { get; set; }
        public long NLCostCentreID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLAccountNumber NLAccountNumber { get; set; }
        public virtual NLCostCentre NLCostCentre { get; set; }
        public virtual ICollection<NLNominalAccount> NLNominalAccounts { get; set; }
    }
}