﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLPendNominalPeriodValue
    {
        public long NLPendNominalPeriodValueID { get; set; }
        public long NLPendNominalAccountID { get; set; }
        public short PeriodNumber { get; set; }
        public decimal ActualThisYearBalance { get; set; }
        public decimal BudgetThisYearBalance { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long NLPendNominalYearValueID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLPendNominalAccount NLPendNominalAccount { get; set; }
        public virtual NLPendNominalYearValue NLPendNominalYearValue { get; set; }
    }
}