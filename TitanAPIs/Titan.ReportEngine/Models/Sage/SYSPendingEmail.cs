// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSPendingEmail
    {
        public long SYSPendingEmailID { get; set; }
        public long SYSEmailTypeID { get; set; }
        public string EmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public long UserID { get; set; }
        public string UserName { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSEmailType SYSEmailType { get; set; }
    }
}