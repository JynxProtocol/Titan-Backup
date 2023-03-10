// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class ReportTrialKittingTask
    {
        public long ReportTrialKittingTaskID { get; set; }
        public long ReportSessionID { get; set; }
        public long BomRecordID { get; set; }
        public long BuildPackageID { get; set; }
        public string StockCode { get; set; }
        public string BomVersion { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public long CumulativeLeadTime { get; set; }
        public string CanBeBuilt { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ReportSession ReportSession { get; set; }
    }
}