// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BackToBackStatus
    {
        public BackToBackStatus()
        {
            SOPOrderReturnLines = new HashSet<SOPOrderReturnLine>();
        }

        public long BackToBackStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SOPOrderReturnLine> SOPOrderReturnLines { get; set; }
    }
}