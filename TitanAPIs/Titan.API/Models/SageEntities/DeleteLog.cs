// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class DeleteLog
    {
        public long DeleteLogID { get; set; }
        public string EntityName { get; set; }
        public string ForeignID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string ParentKey { get; set; }
        public string ParentOpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}