// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SABRE_DLP_OverstockReport
    {
        public string SupplierAccountName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal FreeStockQuantity { get; set; }
        public string ProductGroup { get; set; }
        public decimal QuantityOnOrder { get; set; }
        public string Name { get; set; }
        public decimal MaximumLevel { get; set; }
    }
}