﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NonProductiveBreakdown
    {
        public long ID { get; set; }
        public string Reference { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? Date { get; set; }
        public double? Hours { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public string JobNumber { get; set; }
        public long? JobID { get; set; }
        public long? StageID { get; set; }

        public virtual NonProductiveTime ReferenceNavigation { get; set; }
    }
}