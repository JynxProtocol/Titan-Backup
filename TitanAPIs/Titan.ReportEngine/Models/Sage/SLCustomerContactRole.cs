﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SLCustomerContactRole
    {
        public long SLCustomerContactRoleID { get; set; }
        public long SLCustomerContactID { get; set; }
        public long SYSTraderContactRoleID { get; set; }
        public bool IsPreferredContactForRole { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SLCustomerContact SLCustomerContact { get; set; }
        public virtual SYSTraderContactRole SYSTraderContactRole { get; set; }
    }
}