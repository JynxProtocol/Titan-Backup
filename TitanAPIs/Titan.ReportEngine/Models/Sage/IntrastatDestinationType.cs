﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class IntrastatDestinationType
    {
        public IntrastatDestinationType()
        {
            IntrastatEntryHeaders = new HashSet<IntrastatEntryHeader>();
        }

        public long IntrastatDestinationTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<IntrastatEntryHeader> IntrastatEntryHeaders { get; set; }
    }
}