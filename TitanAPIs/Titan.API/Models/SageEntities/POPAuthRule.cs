﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class POPAuthRule
    {
        public POPAuthRule()
        {
            POPAuthPrincipals = new HashSet<POPAuthPrincipal>();
        }

        public long POPAuthRuleID { get; set; }
        public decimal LessThanValue { get; set; }
        public decimal GreaterThanOrEqualValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long AuthorisationGroup { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public long POPAuthRuleSourceID { get; set; }

        public virtual POPAuthRuleSource POPAuthRuleSource { get; set; }
        public virtual ICollection<POPAuthPrincipal> POPAuthPrincipals { get; set; }
    }
}