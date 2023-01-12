﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SOPAdditionalCharge
    {
        public long SOPAdditionalChargeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal ChargeValue { get; set; }
        public bool ApplyOrderValueDiscount { get; set; }
        public long TaxCodeID { get; set; }
        public long? NominalCodeID { get; set; }
        public decimal NotionalCostValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLNominalAccount NominalCode { get; set; }
        public virtual SYSTaxRate TaxCode { get; set; }
    }
}