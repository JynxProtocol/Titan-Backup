﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLCashFlowReportPeriodHeading
    {
        public long NLCashFlowReportPeriodHeadingID { get; set; }
        public long SYSFinancialYearID { get; set; }
        public DateTime? Period1Heading { get; set; }
        public DateTime? Period2Heading { get; set; }
        public DateTime? Period3Heading { get; set; }
        public DateTime? Period4Heading { get; set; }
        public DateTime? Period5Heading { get; set; }
        public DateTime? Period6Heading { get; set; }
        public DateTime? Period7Heading { get; set; }
        public DateTime? Period8Heading { get; set; }
        public DateTime? Period9Heading { get; set; }
        public DateTime? Period10Heading { get; set; }
        public DateTime? Period11Heading { get; set; }
        public DateTime? Period12Heading { get; set; }
        public DateTime? Period13Heading { get; set; }
        public DateTime? Period14Heading { get; set; }
        public DateTime? Period15Heading { get; set; }
        public DateTime? Period16Heading { get; set; }
        public DateTime? Period17Heading { get; set; }
        public DateTime? Period18Heading { get; set; }
        public DateTime? Period19Heading { get; set; }
        public DateTime? Period20Heading { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSFinancialYear SYSFinancialYear { get; set; }
    }
}