// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLCustomerTurnoverView
    {
        public string mmsslt_PeriodReference { get; set; }
        public DateTime mmsslt_StartDate { get; set; }
        public DateTime mmsslt_EndDate { get; set; }
        public double? mmsslt_TotalInvoiceValueToDate { get; set; }
        public double? mmsslt_TotalCreditNoteValueToDate { get; set; }
        public double? mmsslt_TotalCashValueToDate { get; set; }
        public double? mmsslt_TotalProfitValueToDate { get; set; }
        public double? mmsslt_TotalInvoiceValueToDateInBase { get; set; }
        public double? mmsslt_TotalCredNoteValueToDateInBase { get; set; }
        public double? mmsslt_TotalCashValueToDateInBase { get; set; }
        public double? mmsslt_TotalProfitValueToDateInBase { get; set; }
        public double? mmsslt_ExchangeRateGainOrLoss { get; set; }
        public double? mmsslt_TotalFinanceChargesApplied { get; set; }
        public long mmsslt_SLCustomerPeriodValueID { get; set; }
        public string mmsslt_CustomerAccountNumber { get; set; }
    }
}