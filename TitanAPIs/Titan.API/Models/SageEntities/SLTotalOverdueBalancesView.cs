﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLTotalOverdueBalancesView
    {
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountName { get; set; }
        public string CustomerAccountShortName { get; set; }
        public long SLCustomerAccountID { get; set; }
        public decimal? GoodsValueInAccountCurrencyTotal { get; set; }
        public decimal? AllocatedValueTotal { get; set; }
        public decimal? Balance { get; set; }
    }
}