﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSTraderLocationType
    {
        public SYSTraderLocationType()
        {
            PLFactorHouseLocations = new HashSet<PLFactorHouseLocation>();
            PLSupplierLocations = new HashSet<PLSupplierLocation>();
            SLCustomerLocations = new HashSet<SLCustomerLocation>();
        }

        public long SYSTraderLocationTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PLFactorHouseLocation> PLFactorHouseLocations { get; set; }
        public virtual ICollection<PLSupplierLocation> PLSupplierLocations { get; set; }
        public virtual ICollection<SLCustomerLocation> SLCustomerLocations { get; set; }
    }
}