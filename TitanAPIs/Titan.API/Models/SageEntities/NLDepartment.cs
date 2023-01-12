﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLDepartment
    {
        public NLDepartment()
        {
            NLNominalAccounts = new HashSet<NLNominalAccount>();
        }

        public long NLDepartmentID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public string ContactName { get; set; }
        public string ContactDetails { get; set; }
        public string ContactEmailAddress { get; set; }

        public virtual ICollection<NLNominalAccount> NLNominalAccounts { get; set; }
    }
}