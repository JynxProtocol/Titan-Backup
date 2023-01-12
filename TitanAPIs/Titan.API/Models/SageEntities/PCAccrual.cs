﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PCAccrual
    {
        public long PCAccrualID { get; set; }
        public string Narrative { get; set; }
        public string Reference { get; set; }
        public string SecondReference { get; set; }
        public long PCProjectItemID { get; set; }
        public DateTime? ReversalDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ValueToBill { get; set; }
        public decimal Quantity { get; set; }
        public decimal ChargeRate { get; set; }
        public decimal UnitCost { get; set; }
        public string BSNominalACRef { get; set; }
        public string BSNominalCC { get; set; }
        public string BSNominalDept { get; set; }
        public string PLNominalACRef { get; set; }
        public string PLNominalCC { get; set; }
        public string PLNominalDept { get; set; }
        public string TransactionAnalysisCode { get; set; }
        public long PCUnitOfMeasureID { get; set; }
        public decimal? MarkupPercentage { get; set; }
        public bool IsComplete { get; set; }
        public decimal GoodsValue { get; set; }
        public decimal OverheadValue { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime AccrualDate { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual PCUnitOfMeasure PCUnitOfMeasure { get; set; }
    }
}