﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TEMPriceToleranceBasis
    {
        public TEMPriceToleranceBasis()
        {
            TEMSettings = new HashSet<TEMSetting>();
        }

        public long TEMPriceToleranceBasisID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TEMSetting> TEMSettings { get; set; }
    }
}