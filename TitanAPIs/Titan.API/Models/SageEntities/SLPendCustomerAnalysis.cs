﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLPendCustomerAnalysis
    {
        public long SLPendCustomerAnalysisID { get; set; }
        public long SLPendCustomerAccountID { get; set; }
        public string AnalysisName { get; set; }
        public string AnalysisValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SLPendCustomerAccount SLPendCustomerAccount { get; set; }
    }
}