﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SLPendCustomerBatchTran
    {
        public SLPendCustomerBatchTran()
        {
            SLPendNLAnalysisBatchTrans = new HashSet<SLPendNLAnalysisBatchTran>();
            SLPendTaxAnalysisBatchTrans = new HashSet<SLPendTaxAnalysisBatchTran>();
        }

        public long SLPendCustomerBatchTranID { get; set; }
        public long SLPendCustomerBatchID { get; set; }
        public long SLCustomerAccountID { get; set; }
        public long? CBAccountID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionReference { get; set; }
        public string SecondReference { get; set; }
        public decimal GoodsValueInAccountCurrency { get; set; }
        public decimal TaxValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal DiscountPercentage { get; set; }
        public short DaysDiscountValid { get; set; }
        public decimal AllocatedValue { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public decimal DocumentToBaseCurrencyRate { get; set; }
        public decimal DocumentToAccountExchangeRate { get; set; }
        public decimal ChequeToAccountCurrencyRate { get; set; }
        public decimal BankChequeToBaseCurrencyRate { get; set; }
        public long? ChequeCurrencyID { get; set; }
        public decimal TraderChequeValue { get; set; }
        public string BankNominalAccountNumber { get; set; }
        public string BankNominalCostCentre { get; set; }
        public string BankNominalDepartment { get; set; }
        public string BankNominalAccountName { get; set; }
        public bool AuthorisationFlag { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool TriangularTransaction { get; set; }
        public decimal ESVatDiscountAmount { get; set; }
        public bool IsSettledImmediately { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual CBAccount CBAccount { get; set; }
        public virtual SLCustomerAccount SLCustomerAccount { get; set; }
        public virtual SLPendCustomerBatch SLPendCustomerBatch { get; set; }
        public virtual ICollection<SLPendNLAnalysisBatchTran> SLPendNLAnalysisBatchTrans { get; set; }
        public virtual ICollection<SLPendTaxAnalysisBatchTran> SLPendTaxAnalysisBatchTrans { get; set; }
    }
}