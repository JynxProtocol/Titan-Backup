﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BOMLineType
    {
        public BOMLineType()
        {
            BOMLineCheckedOuts = new HashSet<BOMLineCheckedOut>();
            BOMLines = new HashSet<BOMLine>();
        }

        public long BOMLineTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BOMLineCheckedOut> BOMLineCheckedOuts { get; set; }
        public virtual ICollection<BOMLine> BOMLines { get; set; }
    }
}