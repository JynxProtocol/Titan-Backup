﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepMfgAllocationLine
    {
        public long MFGAllocationLineID { get; set; }
        public long? MFGAllocationID { get; set; }
        public long? ParentAllocationLineID { get; set; }
        public bool? MultiWarehouse { get; set; }
    }
}