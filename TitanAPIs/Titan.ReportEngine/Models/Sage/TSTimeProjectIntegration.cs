// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSTimeProjectIntegration
    {
        public long TSTimeProjectIntegrationID { get; set; }
        public bool? IsIntegrated { get; set; }
        public bool PostAutomatically { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}