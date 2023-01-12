﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSDocCapture
    {
        public long SYSDocCaptureID { get; set; }
        public long SYSDocCaptureTypeID { get; set; }
        public long DocPrimaryID { get; set; }
        public string DocURN { get; set; }
        public string DocReference { get; set; }
        public string DocSecondReference { get; set; }
        public DateTime DocDate { get; set; }
        public long ParentTypeID { get; set; }
        public long ParentPrimaryID { get; set; }
        public string ParentReference { get; set; }
        public string ParentName { get; set; }
        public long CaptureUserID { get; set; }
        public string CaptureUserName { get; set; }
        public string CaptureDescription { get; set; }
        public string CaptureFileName { get; set; }
        public long CaptureFileSize { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSDocCaptureType SYSDocCaptureType { get; set; }
    }
}