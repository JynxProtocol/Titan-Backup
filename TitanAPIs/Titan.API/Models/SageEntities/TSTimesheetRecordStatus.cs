﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TSTimesheetRecordStatus
    {
        public long TSTimesheetRecordStatusID { get; set; }
        public string Comments { get; set; }
        public long? TSTimesheetRecordStatusTypeID { get; set; }
        public string Assigner { get; set; }
        public DateTime WhenModified { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual TSTimesheetRecordStatusType TSTimesheetRecordStatusType { get; set; }
    }
}