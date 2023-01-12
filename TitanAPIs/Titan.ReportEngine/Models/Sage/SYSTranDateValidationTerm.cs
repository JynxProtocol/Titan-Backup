﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSTranDateValidationTerm
    {
        public long SYSTranDateValidationTermsID { get; set; }
        public long TranDateValidationTypeID { get; set; }
        public long ClosedPeriodClassificationID { get; set; }
        public long FuturePeriodClassificationID { get; set; }
        public long FutureYearClassificationID { get; set; }
        public long PreviousYearClassificationID { get; set; }
        public int PostDaysAcceptable { get; set; }
        public int PostDaysNormal { get; set; }
        public int PreDaysAcceptable { get; set; }
        public int PreDaysNormal { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSTranDateValidationClass ClosedPeriodClassification { get; set; }
        public virtual SYSTranDateValidationClass FuturePeriodClassification { get; set; }
        public virtual SYSTranDateValidationClass FutureYearClassification { get; set; }
        public virtual SYSTranDateValidationClass PreviousYearClassification { get; set; }
        public virtual SYSTranDateValidationType TranDateValidationType { get; set; }
    }
}