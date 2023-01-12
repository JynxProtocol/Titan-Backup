﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PLSupplierAccount
    {
        public PLSupplierAccount()
        {
            CBDirectDebitTrans = new HashSet<CBDirectDebitTran>();
            PLAccountMemos = new HashSet<PLAccountMemo>();
            PLAllocationHeaders = new HashSet<PLAllocationHeader>();
            PLHistoricalSupplierTrans = new HashSet<PLHistoricalSupplierTran>();
            PLPendSupplierBatchTrans = new HashSet<PLPendSupplierBatchTran>();
            PLPendSupplierTrans = new HashSet<PLPendSupplierTran>();
            PLPostedSupplierTrans = new HashSet<PLPostedSupplierTran>();
            PLProposedPayments = new HashSet<PLProposedPayment>();
            PLSupplierBanks = new HashSet<PLSupplierBank>();
            PLSupplierContacts = new HashSet<PLSupplierContact>();
            PLSupplierDocuments = new HashSet<PLSupplierDocument>();
            PLSupplierFactorHouses = new HashSet<PLSupplierFactorHouse>();
            PLSupplierLocations = new HashSet<PLSupplierLocation>();
            PLSupplierPeriodValues = new HashSet<PLSupplierPeriodValue>();
            PLSupplierYearValues = new HashSet<PLSupplierYearValue>();
            POPInvCredDisputes = new HashSet<POPInvCredDispute>();
            POPOrderReturnArches = new HashSet<POPOrderReturnArch>();
            POPOrderReturns = new HashSet<POPOrderReturn>();
            POPRequisitionFulfilmentLines = new HashSet<POPRequisitionFulfilmentLine>();
            POPToReorderWarehouses = new HashSet<POPToReorderWarehouse>();
            StockItemSuppliers = new HashSet<StockItemSupplier>();
            TEMSuppProdMappings = new HashSet<TEMSuppProdMapping>();
            TSClaimSheets = new HashSet<TSClaimSheet>();
            TSHumanResources = new HashSet<TSHumanResource>();
        }

        public long PLSupplierAccountID { get; set; }
        public string SupplierAccountNumber { get; set; }
        public string SupplierAccountName { get; set; }
        public string SupplierAccountShortName { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal CreditLimit { get; set; }
        public long SYSCurrencyID { get; set; }
        public long SYSExchangeRateTypeID { get; set; }
        public long SYSCountryCodeID { get; set; }
        public long? PLFactorHouseID { get; set; }
        public long DefaultSYSTaxRateID { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public short MonthsToKeepTransactionsFor { get; set; }
        public string DefaultOrderPriority { get; set; }
        public string DefaultNominalAccountNumber { get; set; }
        public string DefaultNominalCostCentre { get; set; }
        public string DefaultNominalDepartment { get; set; }
        public long SYSAccountTypeID { get; set; }
        public long PLPaymentGroupID { get; set; }
        public decimal EarlySettlementDiscountPercent { get; set; }
        public short DaysEarlySettlementDiscApplies { get; set; }
        public short PaymentTermsInDays { get; set; }
        public long SYSPaymentTermsBasisID { get; set; }
        public bool AccountIsOnHold { get; set; }
        public decimal ValueOfCurrentOrdersInPOP { get; set; }
        public DateTime? DateAccountDetailsLastChanged { get; set; }
        public DateTime? DateOfLastTransaction { get; set; }
        public string EuroAccountNumberCopiedFromTo { get; set; }
        public DateTime? DateEuroAccountCopied { get; set; }
        public bool UseTransactionEMail { get; set; }
        public long? SYSCreditBureauID { get; set; }
        public long? SYSCreditPositionID { get; set; }
        public string TradingTerms { get; set; }
        public string CreditReference { get; set; }
        public DateTime? AccountOpened { get; set; }
        public DateTime? LastCreditReview { get; set; }
        public DateTime? NextCreditReview { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? DateReceived { get; set; }
        public bool TermsAgreed { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
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
        public string MainTelephoneAreaCode { get; set; }
        public string MainTelephoneCountryCode { get; set; }
        public string MainTelephoneSubscriberNumber { get; set; }
        public string MainFaxAreaCode { get; set; }
        public string MainFaxCountryCode { get; set; }
        public string MainFaxSubscriberNumber { get; set; }
        public string MainWebsite { get; set; }
        public string AnalysisCode1 { get; set; }
        public string AnalysisCode2 { get; set; }
        public string AnalysisCode3 { get; set; }
        public string AnalysisCode4 { get; set; }
        public string AnalysisCode5 { get; set; }
        public string AnalysisCode6 { get; set; }
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
        public long SagePaymentsStatusID { get; set; }
        public string SagePaymentsIdentifier { get; set; }
        public bool SagePaymentsHasNote { get; set; }
        public long SYSAccountStatusID { get; set; }
        public string StatusReason { get; set; }

        public virtual SYSTaxRate DefaultSYSTaxRate { get; set; }
        public virtual PLFactorHouse PLFactorHouse { get; set; }
        public virtual PLPaymentGroup PLPaymentGroup { get; set; }
        public virtual SYSAccountStatus SYSAccountStatus { get; set; }
        public virtual SYSAccountType SYSAccountType { get; set; }
        public virtual SYSCountryCode SYSCountryCode { get; set; }
        public virtual SYSCreditBureau SYSCreditBureau { get; set; }
        public virtual SYSCreditPosition SYSCreditPosition { get; set; }
        public virtual SYSCurrency SYSCurrency { get; set; }
        public virtual SYSExchangeRateType SYSExchangeRateType { get; set; }
        public virtual SYSPaymentTermsBasis SYSPaymentTermsBasis { get; set; }
        public virtual SagePaymentsSupplierStatus SagePaymentsStatus { get; set; }
        public virtual TEMSupplierMapping TEMSupplierMapping { get; set; }
        public virtual ICollection<CBDirectDebitTran> CBDirectDebitTrans { get; set; }
        public virtual ICollection<PLAccountMemo> PLAccountMemos { get; set; }
        public virtual ICollection<PLAllocationHeader> PLAllocationHeaders { get; set; }
        public virtual ICollection<PLHistoricalSupplierTran> PLHistoricalSupplierTrans { get; set; }
        public virtual ICollection<PLPendSupplierBatchTran> PLPendSupplierBatchTrans { get; set; }
        public virtual ICollection<PLPendSupplierTran> PLPendSupplierTrans { get; set; }
        public virtual ICollection<PLPostedSupplierTran> PLPostedSupplierTrans { get; set; }
        public virtual ICollection<PLProposedPayment> PLProposedPayments { get; set; }
        public virtual ICollection<PLSupplierBank> PLSupplierBanks { get; set; }
        public virtual ICollection<PLSupplierContact> PLSupplierContacts { get; set; }
        public virtual ICollection<PLSupplierDocument> PLSupplierDocuments { get; set; }
        public virtual ICollection<PLSupplierFactorHouse> PLSupplierFactorHouses { get; set; }
        public virtual ICollection<PLSupplierLocation> PLSupplierLocations { get; set; }
        public virtual ICollection<PLSupplierPeriodValue> PLSupplierPeriodValues { get; set; }
        public virtual ICollection<PLSupplierYearValue> PLSupplierYearValues { get; set; }
        public virtual ICollection<POPInvCredDispute> POPInvCredDisputes { get; set; }
        public virtual ICollection<POPOrderReturnArch> POPOrderReturnArches { get; set; }
        public virtual ICollection<POPOrderReturn> POPOrderReturns { get; set; }
        public virtual ICollection<POPRequisitionFulfilmentLine> POPRequisitionFulfilmentLines { get; set; }
        public virtual ICollection<POPToReorderWarehouse> POPToReorderWarehouses { get; set; }
        public virtual ICollection<StockItemSupplier> StockItemSuppliers { get; set; }
        public virtual ICollection<TEMSuppProdMapping> TEMSuppProdMappings { get; set; }
        public virtual ICollection<TSClaimSheet> TSClaimSheets { get; set; }
        public virtual ICollection<TSHumanResource> TSHumanResources { get; set; }
    }
}