﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLReportCategoryBudgetYearValue
    {
        public long NLReportCategoryBudgetYearValueID { get; set; }
        public long NLReportCategoryBudgetID { get; set; }
        public long SYSFinancialYearID { get; set; }
        public decimal BudgetValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLReportCategoryBudget NLReportCategoryBudget { get; set; }
        public virtual SYSFinancialYear SYSFinancialYear { get; set; }
    }
}