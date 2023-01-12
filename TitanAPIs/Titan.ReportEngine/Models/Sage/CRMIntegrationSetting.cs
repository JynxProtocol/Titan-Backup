﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class CRMIntegrationSetting
    {
        public long CRMIntegrationSettingID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool DisplayProfitabilityInCRM { get; set; }
        public long CRMIntegrationTypeID { get; set; }
        public long CRMIntegrationSyncDateTypeID { get; set; }
        public DateTime? CRMIntegrationSyncDate { get; set; }
        public string CRMMetaDataVersion { get; set; }
        public bool CheckCRMForDelete { get; set; }
        public string CRMWebServiceURL { get; set; }
        public string CRMUsername { get; set; }
        public string CRMPassword { get; set; }
        public int SyncStatus { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public long CRMConfigurationTypeID { get; set; }
        public string CRMENBUGatewayURL { get; set; }

        public virtual CRMConfigurationType CRMConfigurationType { get; set; }
        public virtual CRMIntegrationSyncDateType CRMIntegrationSyncDateType { get; set; }
        public virtual CRMIntegrationType CRMIntegrationType { get; set; }
    }
}