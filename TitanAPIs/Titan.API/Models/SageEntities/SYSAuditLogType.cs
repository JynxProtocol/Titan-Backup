﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSAuditLogType
    {
        public SYSAuditLogType()
        {
            SYSAuditLogHeaders = new HashSet<SYSAuditLogHeader>();
        }

        public long SYSAuditLogTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SYSAuditLogHeader> SYSAuditLogHeaders { get; set; }
    }
}