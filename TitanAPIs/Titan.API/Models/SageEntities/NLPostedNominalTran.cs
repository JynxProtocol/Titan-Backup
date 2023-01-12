﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLPostedNominalTran
    {
        public long NLPostedNominalTranID { get; set; }
        public long NLNominalAccountID { get; set; }
        public long SYSAccountingPeriodID { get; set; }
        public long NLNominalTranTypeID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal GoodsValueInBaseCurrency { get; set; }
        public decimal GoodsValueInDocumentCurrency { get; set; }
        public long DocumentCurrencyID { get; set; }
        public decimal ExchangeRate { get; set; }
        public string Reference { get; set; }
        public string Narrative { get; set; }
        public string UserName { get; set; }
        public int UserNumber { get; set; }
        public long Source { get; set; }
        public long UniqueReferenceNumber { get; set; }
        public DateTime PostedDate { get; set; }
        public string TransactionAnalysisCode { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public long SYSCorrectionTranTypeID { get; set; }

        public virtual SYSCurrency DocumentCurrency { get; set; }
        public virtual NLNominalAccount NLNominalAccount { get; set; }
        public virtual NLNominalTranType NLNominalTranType { get; set; }
        public virtual SYSAccountingPeriod SYSAccountingPeriod { get; set; }
        public virtual SYSCorrectionTranType SYSCorrectionTranType { get; set; }
    }
}