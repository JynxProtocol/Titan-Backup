﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.Models
{
    public partial class Setting
    {
        public int Id { get; set; }
        public string DocumentStorage { get; set; }
        public int LogLevel { get; set; }
        public int CommentLine { get; set; }
        public int DeliveryTermsLine { get; set; }
        public int OrderAck { get; set; }
        public int ContractAuth { get; set; }
        public bool? Wrtest { get; set; }
        public int? RndValue { get; set; }
        public string Postorage { get; set; }
        public string Poprefix { get; set; }
        public int? OrderAckTestEmail { get; set; }
        public string Awkstorage { get; set; }
        public int? PixelSize { get; set; }
        public bool? AwktestMail { get; set; }
        public bool? AwkuseReportEngine { get; set; }
        public bool? OrderAckUseReportEngine { get; set; }
        public bool? AwkemailDirect { get; set; }
        public string TestEmailServer { get; set; }
        public int? TestEmailPort { get; set; }
        public string EmailServer { get; set; }
        public int? EmailPort { get; set; }
        public bool? OrderAckEmailDirect { get; set; }
    }
}