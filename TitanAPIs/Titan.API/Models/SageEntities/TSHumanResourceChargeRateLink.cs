﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TSHumanResourceChargeRateLink
    {
        public long TSHumanResourceChargeRateLinkID { get; set; }
        public long TSHumanResourceID { get; set; }
        public long TSChargeRateID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual TSChargeRate TSChargeRate { get; set; }
        public virtual TSHumanResource TSHumanResource { get; set; }
    }
}