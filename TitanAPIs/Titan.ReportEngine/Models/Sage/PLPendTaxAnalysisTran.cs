﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PLPendTaxAnalysisTran
    {
        public long PLPendTaxAnalysisTranID { get; set; }
        public long PLPendSupplierTranID { get; set; }
        public long SYSTaxRateID { get; set; }
        public decimal GoodsValueBeforeDiscount { get; set; }
        public decimal TaxOnGoodsValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal DiscountPercentage { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal ESVatDiscountAmount { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public string TaxAnalysisDetails { get; set; }

        public virtual PLPendSupplierTran PLPendSupplierTran { get; set; }
        public virtual SYSTaxRate SYSTaxRate { get; set; }
    }
}