﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SOPQuotationLinesView
    {
        public long mmssoql_SOPOrderReturnID { get; set; }
        public int? mmssoql_PrintSequenceNumber { get; set; }
        public string mmssoql_ItemCode { get; set; }
        public string mmssoql_ItemDescription { get; set; }
        public double? mmssoql_LineQuantity { get; set; }
        public double? mmssoql_UnitSellingPrice { get; set; }
        public double? mmssoql_UnitDiscountPercent { get; set; }
        public double? mmssoql_LineTotalValue { get; set; }
        public double? mmssoql_LineTaxValue { get; set; }
        public double? mmssoql_TaxRate { get; set; }
        public short mmssoql_Code { get; set; }
        public string mmssoql_DocumentNo { get; set; }
        public long mmssoql_LineTypeID { get; set; }
    }
}