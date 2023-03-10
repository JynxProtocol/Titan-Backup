// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class ProductGroupUnit
    {
        public long ProductGroupUnitID { get; set; }
        public long ProductGroupID { get; set; }
        public long UnitID { get; set; }
        public long UnitOfMeasureTypeID { get; set; }
        public decimal MultipleOfBaseUnit { get; set; }
        public bool UseOwnPrice { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal UnitPrecision { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual UnitOfMeasureType UnitOfMeasureType { get; set; }
    }
}