﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SABRE_DLP_SOPLinesUnitPrice
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public decimal LineQuantity { get; set; }
        public decimal LineTotalValue { get; set; }
        public decimal DespatchReceiptQuantity { get; set; }
        public DateTime DespatchReceiptDate { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public string CustomerAccountName { get; set; }
    }
}