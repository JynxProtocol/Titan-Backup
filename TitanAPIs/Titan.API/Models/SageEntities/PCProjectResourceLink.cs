﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PCProjectResourceLink
    {
        public long PCProjectResourceLinkID { get; set; }
        public long ProjectID { get; set; }
        public long ResourceID { get; set; }

        public virtual TSHumanResource Resource { get; set; }
    }
}