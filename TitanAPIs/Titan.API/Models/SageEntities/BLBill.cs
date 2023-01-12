﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BLBill
    {
        public BLBill()
        {
            BLBillEvents = new HashSet<BLBillEvent>();
            BLBillLines = new HashSet<BLBillLine>();
            PCProjectEntries = new HashSet<PCProjectEntry>();
        }

        public long BLBillID { get; set; }
        public long SLCustomerID { get; set; }
        public long BLBillHeaderStatusID { get; set; }
        public string Reference { get; set; }
        public decimal ToBaseCurrencyRate { get; set; }
        public short EarlySettlementDiscountDays { get; set; }
        public decimal EarlySettlementDiscount { get; set; }
        public long BLBillDeliveryAddressID { get; set; }
        public bool UseInvoiceAddressAsDeliveryAddress { get; set; }
        public string BillNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal TotalChargesValue { get; set; }
        public decimal TotalDiscountedNetValue { get; set; }
        public decimal TotalDiscountValue { get; set; }
        public decimal TotalGrossValue { get; set; }
        public decimal TotalGrossValueInclSettDisc { get; set; }
        public decimal TotalNetValue { get; set; }
        public decimal TotalTax { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public decimal DocumentDiscountPercent { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BLBillDeliveryAddress BLBillDeliveryAddress { get; set; }
        public virtual BLBillHeaderStatus BLBillHeaderStatus { get; set; }
        public virtual ICollection<BLBillEvent> BLBillEvents { get; set; }
        public virtual ICollection<BLBillLine> BLBillLines { get; set; }
        public virtual ICollection<PCProjectEntry> PCProjectEntries { get; set; }
    }
}