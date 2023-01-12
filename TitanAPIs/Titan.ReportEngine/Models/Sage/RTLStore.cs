﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class RTLStore
    {
        public long StoreId { get; set; }
        public long HierNodeId { get; set; }
        public string StoreName { get; set; }
        public string StoreShortName { get; set; }
        public string Description { get; set; }
        public long? StoreTypeId { get; set; }
        public long? StoreSize { get; set; }
        public long? StoreStatusId { get; set; }
        public long? OperationalStatusId { get; set; }
        public long? LocationTypeId { get; set; }
        public string PostalName { get; set; }
        public string PostalCode { get; set; }
        public string Contact { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public long? ReplMethodId { get; set; }
        public long? ReplSourceId { get; set; }
        public bool AutoRepl { get; set; }
        public long? DelRouteId { get; set; }
        public string NominalDetail1 { get; set; }
        public string NominalDetail2 { get; set; }
        public string NominalDetail3 { get; set; }
        public string NominalDetail4 { get; set; }
        public string Memo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}