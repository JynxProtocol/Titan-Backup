﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class CRMConfigurationType
    {
        public CRMConfigurationType()
        {
            CRMIntegrationSettings = new HashSet<CRMIntegrationSetting>();
        }

        public long CRMConfigurationTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CRMIntegrationSetting> CRMIntegrationSettings { get; set; }
    }
}