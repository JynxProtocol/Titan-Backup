// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class DocumentPrintStatus
    {
        public DocumentPrintStatus()
        {
            POPOrderReturnArches = new HashSet<POPOrderReturnArch>();
            POPOrderReturns = new HashSet<POPOrderReturn>();
            SOPAllocationLines = new HashSet<SOPAllocationLine>();
            SOPOrderReturnArches = new HashSet<SOPOrderReturnArch>();
            SOPOrderReturns = new HashSet<SOPOrderReturn>();
        }

        public long DocumentPrintStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POPOrderReturnArch> POPOrderReturnArches { get; set; }
        public virtual ICollection<POPOrderReturn> POPOrderReturns { get; set; }
        public virtual ICollection<SOPAllocationLine> SOPAllocationLines { get; set; }
        public virtual ICollection<SOPOrderReturnArch> SOPOrderReturnArches { get; set; }
        public virtual ICollection<SOPOrderReturn> SOPOrderReturns { get; set; }
    }
}