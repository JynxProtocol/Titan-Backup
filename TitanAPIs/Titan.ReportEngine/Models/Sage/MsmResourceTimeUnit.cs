﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MsmResourceTimeUnit
    {
        public long MsmResourceTimeUnitID { get; set; }
        /// <summary>
        /// The DB Key of the Time Unit.
        /// </summary>
        public long MsmTimeUnitID { get; set; }
        /// <summary>
        /// The DB Key of the Resource (Labour, Machine etc) that this time unit applies to.
        /// </summary>
        public long ItemID { get; set; }
        public int? Hours { get; set; }
        public byte? Minutes { get; set; }
        public byte? Seconds { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MsmTimeUnit MsmTimeUnit { get; set; }
    }
}