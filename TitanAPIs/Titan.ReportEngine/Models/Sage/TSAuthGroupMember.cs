﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSAuthGroupMember
    {
        public long TSAuthGroupMembersID { get; set; }
        public long TSPersonID { get; set; }
        public long TSAuthorisationGroupID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual TSAuthorisationGroup TSAuthorisationGroup { get; set; }
        public virtual TSPerson TSPerson { get; set; }
    }
}