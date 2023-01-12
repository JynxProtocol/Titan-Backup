﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PSSearch
    {
        public PSSearch()
        {
            PSDisplayFields = new HashSet<PSDisplayField>();
            PSExpressionLines = new HashSet<PSExpressionLine>();
            PSSearchFields = new HashSet<PSSearchField>();
        }

        public long PSSearchID { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool ShowExpressionWithResults { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<PSDisplayField> PSDisplayFields { get; set; }
        public virtual ICollection<PSExpressionLine> PSExpressionLines { get; set; }
        public virtual ICollection<PSSearchField> PSSearchFields { get; set; }
    }
}