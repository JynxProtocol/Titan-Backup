// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class POPAuthRoleType
    {
        public POPAuthRoleType()
        {
            POPAuthPrincipals = new HashSet<POPAuthPrincipal>();
        }

        public long POPAuthRoleTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POPAuthPrincipal> POPAuthPrincipals { get; set; }
    }
}