﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BOMStatusType
    {
        public BOMStatusType()
        {
            BOMCheckedOuts = new HashSet<BOMCheckedOut>();
            BOMs = new HashSet<BOM>();
        }

        public long BOMStatusTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BOMCheckedOut> BOMCheckedOuts { get; set; }
        public virtual ICollection<BOM> BOMs { get; set; }
    }
}