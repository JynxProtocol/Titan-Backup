﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSExchangeRateAmendType
    {
        public SYSExchangeRateAmendType()
        {
            SYSCurrencies = new HashSet<SYSCurrency>();
        }

        public long SYSExchangeRateAmendTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SYSCurrency> SYSCurrencies { get; set; }
    }
}