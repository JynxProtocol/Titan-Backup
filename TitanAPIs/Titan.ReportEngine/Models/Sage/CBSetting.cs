﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class CBSetting
    {
        public long CBSettingID { get; set; }
        public long? DefaultCBAccountID { get; set; }
        public int DefaultChargeDateOffset { get; set; }

        public virtual CBAccount DefaultCBAccount { get; set; }
    }
}