﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MsmFeature
    {
        public MsmFeature()
        {
            MsmUserFeatures = new HashSet<MsmUserFeature>();
        }

        /// <summary>
        /// Primary Key
        /// </summary>
        public long MsmFeatureID { get; set; }
        /// <summary>
        /// Meaningful form/module name for display purposes
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Actual forn name
        /// </summary>
        public string FormName { get; set; }
        public long? ParentFeatureID { get; set; }
        /// <summary>
        /// The ordinal position in which this feature will be displayed.
        /// </summary>
        public decimal? Sequence { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<MsmUserFeature> MsmUserFeatures { get; set; }
    }
}