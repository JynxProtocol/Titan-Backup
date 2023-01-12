﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SLPendCustomerAccount
    {
        public SLPendCustomerAccount()
        {
            SLPendCustomerAnalyses = new HashSet<SLPendCustomerAnalysis>();
        }

        public long SLPendCustomerAccountID { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountName { get; set; }
        public string CustomerAccountShortName { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal CreditLimit { get; set; }
        public string CurrencyISOCode { get; set; }
        public long SYSExchangeRateTypeID { get; set; }
        public string CountryCode { get; set; }
        public long DefaultSYSTaxRateID { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public short MonthsToKeepTransactionsFor { get; set; }
        public string DefaultOrderPriority { get; set; }
        public short? FinanceCharge { get; set; }
        public string DefaultNominalAccountNumber { get; set; }
        public string DefaultNominalCostCentre { get; set; }
        public string DefaultNominalDepartment { get; set; }
        public long SYSAccountTypeID { get; set; }
        public decimal EarlySettlementDiscountPercent { get; set; }
        public short DaysEarlySettlementDiscApplies { get; set; }
        public short PaymentTermsInDays { get; set; }
        public long SYSPaymentTermsBasisID { get; set; }
        public bool UseConsolidatedBilling { get; set; }
        public decimal InvoiceLineDiscountPercent { get; set; }
        public decimal InvoiceDiscountPercent { get; set; }
        public bool SendCopyStatementToBranch { get; set; }
        public bool AccountIsOnHold { get; set; }
        public decimal ValueOfCurrentOrdersInSOP { get; set; }
        public DateTime DateAccountDetailsLastChanged { get; set; }
        public DateTime? DateOfLastTransaction { get; set; }
        public string EuroAccountNumberCopiedFromTo { get; set; }
        public DateTime? DateEuroAccountCopied { get; set; }
        public bool UseTransactionEMail { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }
        public long SYSDocumentLayoutVersionID { get; set; }
        public string TransactionEMail { get; set; }
        public DateTime? DateFinanceChargeLastRun { get; set; }
        public long SLAssociatedOfficeTypeID { get; set; }
        public string AssociatedHeadOfficeAccountNum { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string TradingTerms { get; set; }
        public string CreditReference { get; set; }
        public string CreditBureauName { get; set; }
        public int? AverageTimeToPay { get; set; }
        public string CreditPositionName { get; set; }
        public bool TermsAgreed { get; set; }
        public DateTime? AccountOpened { get; set; }
        public DateTime? LastCreditReview { get; set; }
        public DateTime? NextCreditReview { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? DateReceived { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string MainTelephoneCountryCode { get; set; }
        public string MainTelephoneAreaCode { get; set; }
        public string MainTelephoneSubscriberNumber { get; set; }
        public string MainFaxCountryCode { get; set; }
        public string MainFaxAreaCode { get; set; }
        public string MainFaxSubscriberNumber { get; set; }
        public string MainWebsite { get; set; }
        public string ContactValueTelephoneCountryCode { get; set; }
        public string ContactValueTelephoneAreaCode { get; set; }
        public string ContactValueTelephoneSubscriberNumber { get; set; }
        public string ContactValueFaxCountryCode { get; set; }
        public string ContactValueFaxAreaCode { get; set; }
        public string ContactValueFaxSubscriberNumber { get; set; }
        public string ContactValueMobileCountryCode { get; set; }
        public string ContactValueMobileAreaCode { get; set; }
        public string ContactValueMobileSubscriberNumber { get; set; }
        public string Country { get; set; }
        public string Salutation { get; set; }
        public string ContactValueWebsite { get; set; }
        public string ContactValueEmail { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public long SYSAccountStatusID { get; set; }

        public virtual SYSTaxRate DefaultSYSTaxRate { get; set; }
        public virtual SLOfficeType SLAssociatedOfficeType { get; set; }
        public virtual SYSAccountStatus SYSAccountStatus { get; set; }
        public virtual SYSAccountType SYSAccountType { get; set; }
        public virtual SYSDocumentLayoutVersion SYSDocumentLayoutVersion { get; set; }
        public virtual SYSExchangeRateType SYSExchangeRateType { get; set; }
        public virtual SYSPaymentTermsBasis SYSPaymentTermsBasis { get; set; }
        public virtual ICollection<SLPendCustomerAnalysis> SLPendCustomerAnalyses { get; set; }
    }
}