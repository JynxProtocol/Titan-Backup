// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BomBuildSessionType
    {
        public BomBuildSessionType()
        {
            BomBuildSessions = new HashSet<BomBuildSession>();
        }

        public long BomBuildSessionTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BomBuildSession> BomBuildSessions { get; set; }
    }
}