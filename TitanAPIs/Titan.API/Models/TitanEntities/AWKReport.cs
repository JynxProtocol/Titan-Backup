﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.API.Models
{
    public partial class AWKReport
    {
        public int? ID { get; set; }
        public int AWDID { get; set; }
        public string WorksOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public string CustOrderNumber { get; set; }
        public string AccountName { get; set; }
        public string CatNo { get; set; }
        public string Description { get; set; }
        public string SOR { get; set; }
        public decimal? Qty { get; set; }
        public int? AWKQty { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public bool? Authorised { get; set; }
        public string AuthorisedBy { get; set; }
        public DateTime? DateAuthorised { get; set; }
        public bool? AWKQuoted { get; set; }
        public string AWKQuotedBy { get; set; }
        public DateTime? AWKQuotedDate { get; set; }
        public string ContractName { get; set; }
        public string StockCode { get; set; }
        public string Component { get; set; }
        public int? RequiredQty { get; set; }
        public string Fault { get; set; }
        public string WorkRequired { get; set; }
        public string RepairDetail { get; set; }
        public decimal? RepairCost { get; set; }
        public string ProductGroup { get; set; }
        public bool? SabreStock { get; set; }
        public string AWKSalesType { get; set; }
        public bool? ComAccepted { get; set; }
        public bool? Canceled { get; set; }
    }
}