// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SOPOrderLinesView
    {
        public long mmssool_SOPOrderReturnID { get; set; }
        public int? mmssool_PrintSequenceNumber { get; set; }
        public string mmssool_ItemCode { get; set; }
        public string mmssool_ItemDescription { get; set; }
        public double? mmssool_LineQuantity { get; set; }
        public double? mmssool_UnitSellingPrice { get; set; }
        public double? mmssool_UnitDiscountPercent { get; set; }
        public double? mmssool_LineTotalValue { get; set; }
        public double? mmssool_LineTaxValue { get; set; }
        public double? mmssool_TaxRate { get; set; }
        public short mmssool_Code { get; set; }
        public string mmssool_DocumentNo { get; set; }
        public double? mmssool_AllocatedQuantity { get; set; }
        public double? mmssool_DespatchReceiptQuantity { get; set; }
        public double? mmssool_InvoiceCreditQuantity { get; set; }
        public long? mmssool_POPOrderReturnLineID { get; set; }
        public string mmssool_POPOrder { get; set; }
        public string mmssool_Complete { get; set; }
        public long mmssool_LineTypeID { get; set; }
    }
}