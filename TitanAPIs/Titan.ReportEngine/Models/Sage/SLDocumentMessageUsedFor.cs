﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SLDocumentMessageUsedFor
    {
        public SLDocumentMessageUsedFor()
        {
            SLDocumentMessages = new HashSet<SLDocumentMessage>();
        }

        public long SLDocumentMessageUsedForID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SLDocumentMessage> SLDocumentMessages { get; set; }
    }
}