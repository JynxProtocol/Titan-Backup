﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MFGContactValue
    {
        public long MFGContactValueID { get; set; }
        public long SYSContactTypeID { get; set; }
        public long MFGContactID { get; set; }
        public string ContactValue { get; set; }
        public bool IsPreferred { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string ContactValueCountryCode { get; set; }
        public string ContactValueAreaCode { get; set; }
        public string ContactValueSubscriberNumber { get; set; }
        public string ContactValuePreMigratedData { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MFGContact MFGContact { get; set; }
        public virtual SYSContactType SYSContactType { get; set; }
    }
}