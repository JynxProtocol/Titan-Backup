﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BOMCostsBooked
    {
        public long BOMCostsBookedID { get; set; }
        public long BOMBuildID { get; set; }
        public long? NominalCodeID { get; set; }
        public string NominalAccount { get; set; }
        public string CostCentre { get; set; }
        public string Department { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BOMBuild BOMBuild { get; set; }
    }
}