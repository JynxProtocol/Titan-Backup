﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSCurrency
    {
        public SYSCurrency()
        {
            CBAccounts = new HashSet<CBAccount>();
            CBPostedAccountTrans = new HashSet<CBPostedAccountTran>();
            NLDeferredNominalTrans = new HashSet<NLDeferredNominalTran>();
            NLHistoricalNominalTrans = new HashSet<NLHistoricalNominalTran>();
            NLPendNominalTrans = new HashSet<NLPendNominalTran>();
            NLPostedNominalTrans = new HashSet<NLPostedNominalTran>();
            OrderValueDiscounts = new HashSet<OrderValueDiscount>();
            PCProjectEntries = new HashSet<PCProjectEntry>();
            PLPendSupplierBatchTrans = new HashSet<PLPendSupplierBatchTran>();
            PLPendSupplierTrans = new HashSet<PLPendSupplierTran>();
            PLSupplierAccounts = new HashSet<PLSupplierAccount>();
            PriceBands = new HashSet<PriceBand>();
            SLCustomerAccounts = new HashSet<SLCustomerAccount>();
            SOPOrderReturnArches = new HashSet<SOPOrderReturnArch>();
            SOPOrderReturns = new HashSet<SOPOrderReturn>();
            SOPPaymentMethods = new HashSet<SOPPaymentMethod>();
            SYSExchangeRateHistories = new HashSet<SYSExchangeRateHistory>();
            SYSPeriodExchangeRates = new HashSet<SYSPeriodExchangeRate>();
            StockItemDiscounts = new HashSet<StockItemDiscount>();
            TSClaimSheets = new HashSet<TSClaimSheet>();
        }

        public long SYSCurrencyID { get; set; }
        public long SYSExchangeRateAmendTypeID { get; set; }
        public long SYSExchangeRateTypeID { get; set; }
        public long SYSCurrencyISOCodeID { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal OneUnitBaseEquals { get; set; }
        public decimal OneEuroEquals { get; set; }
        public decimal AccumulatedExchangeRateGain { get; set; }
        public string NominalAccountNumber { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public bool ThisIsBaseCurrency { get; set; }
        public bool ThisIsEuroCurrency { get; set; }
        public bool? UseForNewAccounts { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public bool IsSagePaymentsCurrency { get; set; }
        public decimal SagePaymentsRate { get; set; }

        public virtual SYSCurrencyISOCode SYSCurrencyISOCode { get; set; }
        public virtual SYSExchangeRateAmendType SYSExchangeRateAmendType { get; set; }
        public virtual SYSExchangeRateType SYSExchangeRateType { get; set; }
        public virtual ICollection<CBAccount> CBAccounts { get; set; }
        public virtual ICollection<CBPostedAccountTran> CBPostedAccountTrans { get; set; }
        public virtual ICollection<NLDeferredNominalTran> NLDeferredNominalTrans { get; set; }
        public virtual ICollection<NLHistoricalNominalTran> NLHistoricalNominalTrans { get; set; }
        public virtual ICollection<NLPendNominalTran> NLPendNominalTrans { get; set; }
        public virtual ICollection<NLPostedNominalTran> NLPostedNominalTrans { get; set; }
        public virtual ICollection<OrderValueDiscount> OrderValueDiscounts { get; set; }
        public virtual ICollection<PCProjectEntry> PCProjectEntries { get; set; }
        public virtual ICollection<PLPendSupplierBatchTran> PLPendSupplierBatchTrans { get; set; }
        public virtual ICollection<PLPendSupplierTran> PLPendSupplierTrans { get; set; }
        public virtual ICollection<PLSupplierAccount> PLSupplierAccounts { get; set; }
        public virtual ICollection<PriceBand> PriceBands { get; set; }
        public virtual ICollection<SLCustomerAccount> SLCustomerAccounts { get; set; }
        public virtual ICollection<SOPOrderReturnArch> SOPOrderReturnArches { get; set; }
        public virtual ICollection<SOPOrderReturn> SOPOrderReturns { get; set; }
        public virtual ICollection<SOPPaymentMethod> SOPPaymentMethods { get; set; }
        public virtual ICollection<SYSExchangeRateHistory> SYSExchangeRateHistories { get; set; }
        public virtual ICollection<SYSPeriodExchangeRate> SYSPeriodExchangeRates { get; set; }
        public virtual ICollection<StockItemDiscount> StockItemDiscounts { get; set; }
        public virtual ICollection<TSClaimSheet> TSClaimSheets { get; set; }
    }
}