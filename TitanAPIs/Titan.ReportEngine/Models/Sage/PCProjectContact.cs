﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PCProjectContact
    {
        public long PCProjectContactID { get; set; }
        public long PCProjectItemID { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public string TelephoneNumber { get; set; }
        public string PostalName { get; set; }
        public string ContactAddress1 { get; set; }
        public string ContactAddress2 { get; set; }
        public string ContactAddress3 { get; set; }
        public string ContactAddress4 { get; set; }
        public string PostCode { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public bool? DeliveryAddress { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public string ContactCity { get; set; }
        public string ContactCounty { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}