﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class POPRequisitionLine
    {
        public POPRequisitionLine()
        {
            POPRequisitionFulfilmentLines = new HashSet<POPRequisitionFulfilmentLine>();
        }

        public long POPRequisitionLineID { get; set; }
        public long POPRequisitionID { get; set; }
        public long POPRequisitionStatusID { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string WarehouseName { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityToFulfil { get; set; }
        public decimal QuantityFulfilled { get; set; }
        public string TaxRateName { get; set; }
        public decimal TaxRate { get; set; }
        public string NominalAccountName { get; set; }
        public string NominalAccountRef { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public string BuyingUnitDescription { get; set; }
        public decimal UnitBuyingPrice { get; set; }
        public decimal NetValue { get; set; }
        public decimal ExchangeRate { get; set; }
        public string CurrencySymbol { get; set; }
        public long? AuthoriserUserID { get; set; }
        public string AuthoriserUserName { get; set; }
        public DateTime? AuthorisedDate { get; set; }
        public string AuthoriserComment { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public long? NLReportCategoryBudgetID { get; set; }

        public virtual NLReportCategoryBudget NLReportCategoryBudget { get; set; }
        public virtual POPRequisition POPRequisition { get; set; }
        public virtual POPRequisitionStatus POPRequisitionStatus { get; set; }
        public virtual ICollection<POPRequisitionFulfilmentLine> POPRequisitionFulfilmentLines { get; set; }
    }
}