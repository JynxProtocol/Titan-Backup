﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSContactType
    {
        public SYSContactType()
        {
            MFGContactValues = new HashSet<MFGContactValue>();
            PLFactorHouseContacts = new HashSet<PLFactorHouseContact>();
            PLSupplierContactValues = new HashSet<PLSupplierContactValue>();
            SLCustomerContactValues = new HashSet<SLCustomerContactValue>();
            SYSCompanyContacts = new HashSet<SYSCompanyContact>();
        }

        public long SYSContactTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MFGContactValue> MFGContactValues { get; set; }
        public virtual ICollection<PLFactorHouseContact> PLFactorHouseContacts { get; set; }
        public virtual ICollection<PLSupplierContactValue> PLSupplierContactValues { get; set; }
        public virtual ICollection<SLCustomerContactValue> SLCustomerContactValues { get; set; }
        public virtual ICollection<SYSCompanyContact> SYSCompanyContacts { get; set; }
    }
}