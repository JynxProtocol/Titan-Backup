// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class WorksOrderSerialNumber
    {
        public long ID { get; set; }
        public long? HeaderID { get; set; }
        public long? FinishedItem { get; set; }
        public string SerialNumber { get; set; }
        public long? SOrderNumber { get; set; }
        public string AccountRef { get; set; }
        public DateTime? DateDespatched { get; set; }

        public virtual WorksOrder Header { get; set; }
    }
}