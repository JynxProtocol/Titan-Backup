﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLDefaultNominalAccount
    {
        public long NLDefaultNominalAccountID { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public long? NLAccountReportTypeID { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public bool UseSpecified { get; set; }
        public long? NLNominalAccountID { get; set; }
        public bool UsedInStandard { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLAccountReportType NLAccountReportType { get; set; }
        public virtual NLNominalAccount NLNominalAccount { get; set; }
    }
}