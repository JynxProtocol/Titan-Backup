﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SABRE_DLP_DownloadReportKANBAN
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string AnalysisCode2 { get; set; }
        public decimal LastCostPrice { get; set; }
        public decimal? _1Month { get; set; }
        public decimal? _3Month { get; set; }
        public decimal? _12Month { get; set; }
        public short LeadTime { get; set; }
        public decimal MinimumOrderQuantity { get; set; }
        public string SupplierAccountName { get; set; }
        public decimal LastOrderQuantity { get; set; }
        public string KanBanQty { get; set; }
        public decimal QuantityOnPOPOrder { get; set; }
        public decimal? FreeStock { get; set; }
        public string BinName { get; set; }
    }
}