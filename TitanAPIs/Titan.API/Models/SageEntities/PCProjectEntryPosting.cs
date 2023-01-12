﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PCProjectEntryPosting
    {
        public PCProjectEntryPosting()
        {
            BLBillLinePostings = new HashSet<BLBillLinePosting>();
            BLBilledTransactions = new HashSet<BLBilledTransaction>();
        }

        public long PCProjectEntryPostingID { get; set; }
        public long PCProjectEntryID { get; set; }
        public decimal AmountDocumentCurrency { get; set; }
        public DateTime ProjectEntryPostingDate { get; set; }
        public long SYSCurrencyID { get; set; }
        public decimal AmountBaseCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Quantity { get; set; }
        public long? BLBillLineID { get; set; }
        public long PCEntryPostingStatusID { get; set; }
        public long? INVInvoiceCreditLineID { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual INVInvoiceCreditLine INVInvoiceCreditLine { get; set; }
        public virtual PCEntryPostingStatus PCEntryPostingStatus { get; set; }
        public virtual PCProjectEntry PCProjectEntry { get; set; }
        public virtual ICollection<BLBillLinePosting> BLBillLinePostings { get; set; }
        public virtual ICollection<BLBilledTransaction> BLBilledTransactions { get; set; }
    }
}