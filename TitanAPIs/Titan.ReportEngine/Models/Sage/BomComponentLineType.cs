﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BomComponentLineType
    {
        public BomComponentLineType()
        {
            BomComponentLines = new HashSet<BomComponentLine>();
        }

        public long BomComponentLineTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BomComponentLine> BomComponentLines { get; set; }
    }
}