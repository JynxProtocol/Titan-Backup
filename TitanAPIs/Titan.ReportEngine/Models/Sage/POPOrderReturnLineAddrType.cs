// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class POPOrderReturnLineAddrType
    {
        public POPOrderReturnLineAddrType()
        {
            POPOrdReturnLineDelAddrArches = new HashSet<POPOrdReturnLineDelAddrArch>();
            POPOrdReturnLineDelAddresses = new HashSet<POPOrdReturnLineDelAddress>();
        }

        public long POPOrderReturnLineAddrTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POPOrdReturnLineDelAddrArch> POPOrdReturnLineDelAddrArches { get; set; }
        public virtual ICollection<POPOrdReturnLineDelAddress> POPOrdReturnLineDelAddresses { get; set; }
    }
}