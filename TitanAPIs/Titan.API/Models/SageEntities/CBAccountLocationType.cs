// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CBAccountLocationType
    {
        public CBAccountLocationType()
        {
            CBBankLocations = new HashSet<CBBankLocation>();
        }

        public long CBAccountLocationTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CBBankLocation> CBBankLocations { get; set; }
    }
}