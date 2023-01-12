﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class POPOrderReturn
    {
        public POPOrderReturn()
        {
            POPCancelledLines = new HashSet<POPCancelledLine>();
            POPOrderAuthorisers = new HashSet<POPOrderAuthoriser>();
            POPOrderNotifications = new HashSet<POPOrderNotification>();
            POPOrderReturnLines = new HashSet<POPOrderReturnLine>();
        }

        public long POPOrderReturnID { get; set; }
        public long DocumentTypeID { get; set; }
        public string DocumentNo { get; set; }
        public DateTime? DocumentDate { get; set; }
        public long DocumentStatusID { get; set; }
        public long DocumentPrintStatusID { get; set; }
        public string SupplierDocumentNo { get; set; }
        public long? SupplierID { get; set; }
        public DateTime? RequestedDeliveryDate { get; set; }
        public long? WarehouseID { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal SubtotalGoodsValue { get; set; }
        public decimal SubtotalChargesNetValue { get; set; }
        public decimal SubtotalChargesTaxValue { get; set; }
        public decimal SubtotalDiscountValue { get; set; }
        public decimal TotalNetValue { get; set; }
        public decimal TotalTaxValue { get; set; }
        public decimal TotalGrossValue { get; set; }
        public decimal TotalAccrualValue { get; set; }
        public decimal SettlementDiscPercent { get; set; }
        public short SettlementDiscountDays { get; set; }
        public decimal DocumentDiscountPercent { get; set; }
        public string DocumentCreatedBy { get; set; }
        public string AnalysisCode1 { get; set; }
        public string AnalysisCode2 { get; set; }
        public string AnalysisCode3 { get; set; }
        public string AnalysisCode4 { get; set; }
        public string AnalysisCode5 { get; set; }
        public string AnalysisCode6 { get; set; }
        public short? IntrastatStatus { get; set; }
        public long SourceTypeID { get; set; }
        public string SourceDocumentNo { get; set; }
        public long AuthorisationStatusID { get; set; }
        public bool SingleDeliveryAddress { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal SubtotalLandedCosts { get; set; }
        public string SpareText1 { get; set; }
        public string SpareText2 { get; set; }
        public string SpareText3 { get; set; }
        public decimal SpareNumber1 { get; set; }
        public decimal SpareNumber2 { get; set; }
        public decimal SpareNumber3 { get; set; }
        public DateTime? SpareDate1 { get; set; }
        public DateTime? SpareDate2 { get; set; }
        public DateTime? SpareDate3 { get; set; }
        public bool SpareBit1 { get; set; }
        public bool SpareBit2 { get; set; }
        public bool SpareBit3 { get; set; }
        public long? DefaultDeliveryAddressID { get; set; }
        public long? CRMAccount { get; set; }
        public long? DocumentCreatedByUserID { get; set; }
        public string DocumentOriginatorName { get; set; }
        public long? DocumentOriginatorID { get; set; }
        public string AnalysisCode7 { get; set; }
        public string AnalysisCode8 { get; set; }
        public string AnalysisCode9 { get; set; }
        public string AnalysisCode10 { get; set; }
        public string AnalysisCode11 { get; set; }
        public string AnalysisCode12 { get; set; }
        public string AnalysisCode13 { get; set; }
        public string AnalysisCode14 { get; set; }
        public string AnalysisCode15 { get; set; }
        public string AnalysisCode16 { get; set; }
        public string AnalysisCode17 { get; set; }
        public string AnalysisCode18 { get; set; }
        public string AnalysisCode19 { get; set; }
        public string AnalysisCode20 { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual AuthorisationStatus AuthorisationStatus { get; set; }
        public virtual POPOrdReturnLineDelAddress DefaultDeliveryAddress { get; set; }
        public virtual DocumentPrintStatus DocumentPrintStatus { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }
        public virtual POPOrderReturnType DocumentType { get; set; }
        public virtual POPOrderReturnType SourceType { get; set; }
        public virtual PLSupplierAccount Supplier { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual POPDocDelAddress POPDocDelAddress { get; set; }
        public virtual ICollection<POPCancelledLine> POPCancelledLines { get; set; }
        public virtual ICollection<POPOrderAuthoriser> POPOrderAuthorisers { get; set; }
        public virtual ICollection<POPOrderNotification> POPOrderNotifications { get; set; }
        public virtual ICollection<POPOrderReturnLine> POPOrderReturnLines { get; set; }
    }
}