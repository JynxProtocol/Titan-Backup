﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MovementBalanceType
    {
        public MovementBalanceType()
        {
            MovementBalances = new HashSet<MovementBalance>();
        }

        public long MovementBalanceTypeID { get; set; }
        public string MovementBalanceTypeName { get; set; }

        public virtual ICollection<MovementBalance> MovementBalances { get; set; }
    }
}