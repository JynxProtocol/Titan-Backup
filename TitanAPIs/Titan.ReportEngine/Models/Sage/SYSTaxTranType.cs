﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSTaxTranType
    {
        public SYSTaxTranType()
        {
            SYSTaxTrans = new HashSet<SYSTaxTran>();
        }

        public long SYSTaxTranTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SYSTaxTran> SYSTaxTrans { get; set; }
    }
}