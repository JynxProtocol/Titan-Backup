// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PLTotalOverdueBalancesView
    {
        public string SupplierAccountNumber { get; set; }
        public string SupplierAccountName { get; set; }
        public string SupplierAccountShortName { get; set; }
        public long PLSupplierAccountID { get; set; }
        public decimal? GoodsValueInAccountCurrencyTotal { get; set; }
        public decimal? AllocatedValueTotal { get; set; }
        public decimal? Balance { get; set; }
    }
}