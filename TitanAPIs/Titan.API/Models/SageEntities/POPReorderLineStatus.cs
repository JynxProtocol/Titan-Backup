// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class POPReorderLineStatus
    {
        public POPReorderLineStatus()
        {
            POPToReorderSOPLines = new HashSet<POPToReorderSOPLine>();
        }

        public long POPReorderLineStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POPToReorderSOPLine> POPToReorderSOPLines { get; set; }
    }
}