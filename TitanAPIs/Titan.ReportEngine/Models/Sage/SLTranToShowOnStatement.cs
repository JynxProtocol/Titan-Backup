﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SLTranToShowOnStatement
    {
        public SLTranToShowOnStatement()
        {
            SLSettings = new HashSet<SLSetting>();
        }

        public long SLTranToShowOnStatementsID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SLSetting> SLSettings { get; set; }
    }
}