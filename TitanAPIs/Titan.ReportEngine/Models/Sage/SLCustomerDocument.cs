﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SLCustomerDocument
    {
        public long SLCustomerDocumentID { get; set; }
        public long SLCustomerAccountID { get; set; }
        public long SYSDocumentTypeID { get; set; }
        public long SYSDocTransmissionMethodID { get; set; }
        public long? SendToSLCustomerLocationID { get; set; }
        public long SYSDocumentLayoutVersionID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long? SendToSLCustContactValueID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SLCustomerAccount SLCustomerAccount { get; set; }
        public virtual SYSDocTransmissionMethod SYSDocTransmissionMethod { get; set; }
        public virtual SYSDocumentLayoutVersion SYSDocumentLayoutVersion { get; set; }
        public virtual SYSDocumentType SYSDocumentType { get; set; }
        public virtual SLCustomerContactValue SendToSLCustContactValue { get; set; }
        public virtual SLCustomerLocation SendToSLCustomerLocation { get; set; }
    }
}