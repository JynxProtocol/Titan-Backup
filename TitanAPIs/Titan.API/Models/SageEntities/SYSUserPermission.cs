// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSUserPermission
    {
        public long SYSUserPermissionID { get; set; }
        public long SYSUserID { get; set; }
        public bool AddAmendDeletetPeriods { get; set; }
        public bool OpenFuturePeriodsSales { get; set; }
        public bool OpenFuturePeriodsPurchases { get; set; }
        public bool OpenFuturePeriodsCashBook { get; set; }
        public bool OpenFuturePeriodsStock { get; set; }
        public bool OpenFuturePeriodsNLAdjustments { get; set; }
        public bool ClosePeriodsSales { get; set; }
        public bool ClosePeriodsPurchases { get; set; }
        public bool ClosePeriodsCashBook { get; set; }
        public bool ClosePeriodsStock { get; set; }
        public bool ClosePeriodsNominalAdjustments { get; set; }
        public bool ReOpenPeriodsSales { get; set; }
        public bool ReOpenPeriodsPurchases { get; set; }
        public bool ReOpenPeriodsCashBook { get; set; }
        public bool ReOpenPeriodsStock { get; set; }
        public bool ReOpenPeriodsNLAdjustments { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSUser SYSUser { get; set; }
    }
}