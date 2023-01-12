﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSExpressionLine
    {
        public long SYSExpressionLineID { get; set; }
        public long SYSSearchID { get; set; }
        public long SYSLogicalOperatorDescriptorID { get; set; }
        public long SYSCondOperatorDescriptorID { get; set; }
        public long SYSFieldDescriptorID { get; set; }
        public string Value { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSCondOperatorDescriptor SYSCondOperatorDescriptor { get; set; }
        public virtual SYSFieldDescriptor SYSFieldDescriptor { get; set; }
        public virtual SYSLogicalOperatorDescriptor SYSLogicalOperatorDescriptor { get; set; }
        public virtual SYSSearch SYSSearch { get; set; }
    }
}