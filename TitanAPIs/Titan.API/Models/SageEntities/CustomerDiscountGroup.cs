﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CustomerDiscountGroup
    {
        public CustomerDiscountGroup()
        {
            CustProdGroupDiscounts = new HashSet<CustProdGroupDiscount>();
            CustStockItemDiscounts = new HashSet<CustStockItemDiscount>();
            SLCustomerAccounts = new HashSet<SLCustomerAccount>();
        }

        public long CustomerDiscountGroupID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool? IsSingleCustomerGroup { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<CustProdGroupDiscount> CustProdGroupDiscounts { get; set; }
        public virtual ICollection<CustStockItemDiscount> CustStockItemDiscounts { get; set; }
        public virtual ICollection<SLCustomerAccount> SLCustomerAccounts { get; set; }
    }
}