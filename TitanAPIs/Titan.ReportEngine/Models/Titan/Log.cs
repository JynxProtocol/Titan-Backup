// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.ReportEngine.Models.Titan
{
    public partial class Log
    {
        public int logID { get; set; }
        public string logModule { get; set; }
        public string logDetails { get; set; }
        public DateTime? logDate { get; set; }
        public string UsrName { get; set; }
        public int? severity { get; set; }
    }
}