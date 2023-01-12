﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PLSupplierContact
    {
        public PLSupplierContact()
        {
            PLSupplierContactRoles = new HashSet<PLSupplierContactRole>();
            PLSupplierContactValues = new HashSet<PLSupplierContactValue>();
        }

        public long PLSupplierContactID { get; set; }
        public long PLSupplierAccountID { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long PLSupplierLocationID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public long SalutationID { get; set; }
        public string ContactNamePreMigratedData { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual PLSupplierAccount PLSupplierAccount { get; set; }
        public virtual PLSupplierLocation PLSupplierLocation { get; set; }
        public virtual Salutation Salutation { get; set; }
        public virtual ICollection<PLSupplierContactRole> PLSupplierContactRoles { get; set; }
        public virtual ICollection<PLSupplierContactValue> PLSupplierContactValues { get; set; }
    }
}