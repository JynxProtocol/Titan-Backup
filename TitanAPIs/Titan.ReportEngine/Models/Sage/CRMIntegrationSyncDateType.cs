// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class CRMIntegrationSyncDateType
    {
        public CRMIntegrationSyncDateType()
        {
            CRMIntegrationSettings = new HashSet<CRMIntegrationSetting>();
        }

        public long CRMIntegrationSyncDateTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CRMIntegrationSetting> CRMIntegrationSettings { get; set; }
    }
}