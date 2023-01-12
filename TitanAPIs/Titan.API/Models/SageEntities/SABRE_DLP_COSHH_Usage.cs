﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SABRE_DLP_COSHH_Usage
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ProductGroup { get; set; }
        public string IssueMethod { get; set; }
        public string ProductClassification { get; set; }
        public bool? MadeInHouse { get; set; }
        public bool? ISIRRequired { get; set; }
        public bool? CofCRequired { get; set; }
        public bool? MSDSRequired { get; set; }
        public bool? MSDSApproved { get; set; }
        public string MSDSApprovedBy { get; set; }
        public DateTime? MSDSApprovedDate { get; set; }
        public bool? RiskAssessmentReqd { get; set; }
        public decimal ConfirmedQtyInStock { get; set; }
        public decimal? _1Month { get; set; }
        public decimal? _3Month { get; set; }
        public decimal? _12Month { get; set; }
        public string WareHouse { get; set; }
    }
}