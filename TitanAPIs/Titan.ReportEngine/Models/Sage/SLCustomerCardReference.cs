﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SLCustomerCardReference
    {
        public long SLCustomerCardReferenceID { get; set; }
        public long SLCustomerAccountID { get; set; }
        public string Description { get; set; }
        public DateTime LastUsedDate { get; set; }
        public string TranCode { get; set; }
        public string AuthNumber { get; set; }
        public string SecurityKey { get; set; }
        public string CardProcessorTranCode { get; set; }

        public virtual SLCustomerAccount SLCustomerAccount { get; set; }
    }
}