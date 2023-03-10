// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SOPInvoiceCreditLine
    {
        public SOPInvoiceCreditLine()
        {
            InvoiceLineProfitAnalyses = new HashSet<InvoiceLineProfitAnalysis>();
            SOPDespatchReceiptLines = new HashSet<SOPDespatchReceiptLine>();
            SOPPaymentInvCredLines = new HashSet<SOPPaymentInvCredLine>();
            TraceSOPInvCredLines = new HashSet<TraceSOPInvCredLine>();
        }

        public long SOPInvoiceCreditLineID { get; set; }
        public long SOPInvoiceCreditID { get; set; }
        public long? SOPOrderReturnLineID { get; set; }
        public short PrintSequenceNumber { get; set; }
        public string DespatchReceiptNos { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public decimal InvoiceCreditQuantity { get; set; }
        public DateTime InvoiceCreditDate { get; set; }
        public decimal OutstandingQuantity { get; set; }
        public decimal LineTotalValue { get; set; }
        public decimal LineTaxValue { get; set; }
        public decimal LineDiscountPercent { get; set; }
        public string OrderReturnNo { get; set; }
        public bool? OrderReturnArchived { get; set; }
        public long? TEMMessageID { get; set; }
        public short? TEMmessageLineNo { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal ItemPrice { get; set; }
        public long? SYSTaxRateID { get; set; }
        public long? SOPOrderReturnID { get; set; }
        public string ItemName { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public string NominalAccountNumber { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public string ItemCode { get; set; }
        public string ItemUnitOfMeasure { get; set; }

        public virtual SOPInvoiceCredit SOPInvoiceCredit { get; set; }
        public virtual SOPOrderReturn SOPOrderReturn { get; set; }
        public virtual SOPOrderReturnLine SOPOrderReturnLine { get; set; }
        public virtual SYSTaxRate SYSTaxRate { get; set; }
        public virtual ICollection<InvoiceLineProfitAnalysis> InvoiceLineProfitAnalyses { get; set; }
        public virtual ICollection<SOPDespatchReceiptLine> SOPDespatchReceiptLines { get; set; }
        public virtual ICollection<SOPPaymentInvCredLine> SOPPaymentInvCredLines { get; set; }
        public virtual ICollection<TraceSOPInvCredLine> TraceSOPInvCredLines { get; set; }
    }
}