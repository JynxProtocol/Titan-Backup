﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSExchangeRateAction
    {
        public SYSExchangeRateAction()
        {
            SYSExchangeRateHistories = new HashSet<SYSExchangeRateHistory>();
        }

        public long SYSExchangeRateActionID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SYSExchangeRateHistory> SYSExchangeRateHistories { get; set; }
    }
}