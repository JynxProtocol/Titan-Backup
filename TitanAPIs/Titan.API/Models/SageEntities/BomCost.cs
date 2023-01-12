﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BomCost
    {
        public long BomCostID { get; set; }
        public long BomCostSessionID { get; set; }
        public long BomCostTypeID { get; set; }
        public long? MsmCostHeadingID { get; set; }
        public long MsmCostHeadingTypeID { get; set; }
        public string CostHeadingName { get; set; }
        public string OperationReference { get; set; }
        public string OperationDescription { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal UnitCost { get; set; }
        public string StockUnit { get; set; }
        public decimal? StockQuantity { get; set; }
        public string Units { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Overhead { get; set; }
        public decimal Markup { get; set; }
        public decimal OverheadPercentage { get; set; }
        public decimal MarkupPercentage { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Profit { get; set; }
        public decimal MarginPercentage { get; set; }
        public long? SubassemblyCostSessionID { get; set; }
        public bool? Consumed { get; set; }
        public long? OpOperationPhaseID { get; set; }
        public bool FixedCost { get; set; }
        public bool? CurrencyConversionPerformed { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string ForeignCurrencyName { get; set; }
        public string ForeignCurrencySymbol { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BomCostSession BomCostSession { get; set; }
        public virtual BomCostType BomCostType { get; set; }
        public virtual MsmCostHeading MsmCostHeading { get; set; }
        public virtual MsmCostHeadingType MsmCostHeadingType { get; set; }
        public virtual OpOperationPhase OpOperationPhase { get; set; }
    }
}