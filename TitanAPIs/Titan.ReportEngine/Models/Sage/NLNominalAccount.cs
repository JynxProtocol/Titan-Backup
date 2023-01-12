﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLNominalAccount
    {
        public NLNominalAccount()
        {
            CBDirectDebitTrans = new HashSet<CBDirectDebitTran>();
            InternalAreas = new HashSet<InternalArea>();
            InverseParentNLNominalAccount = new HashSet<NLNominalAccount>();
            MFGIssueCreditNominals = new HashSet<MFGIssue>();
            MFGIssueDebitNominals = new HashSet<MFGIssue>();
            NLAccountMemos = new HashSet<NLAccountMemo>();
            NLAccountPeriodValues = new HashSet<NLAccountPeriodValue>();
            NLAccountYearValues = new HashSet<NLAccountYearValue>();
            NLCashFlowLayoutNominalAccounts = new HashSet<NLCashFlowLayoutNominalAccount>();
            NLDefaultNominalAccounts = new HashSet<NLDefaultNominalAccount>();
            NLPostedNominalTrans = new HashSet<NLPostedNominalTran>();
            NLReconciliationEnquirySettings = new HashSet<NLReconciliationEnquirySetting>();
            POPAdditionalCharges = new HashSet<POPAdditionalCharge>();
            POPSettings = new HashSet<POPSetting>();
            ProdGroupNominalCodes = new HashSet<ProdGroupNominalCode>();
            SOPAdditionalCharges = new HashSet<SOPAdditionalCharge>();
            SOPOrderReturnArches = new HashSet<SOPOrderReturnArch>();
            SOPOrderReturns = new HashSet<SOPOrderReturn>();
            SOPPaymentMethods = new HashSet<SOPPaymentMethod>();
            StockItemNominalCodes = new HashSet<StockItemNominalCode>();
            StockSettings = new HashSet<StockSetting>();
            Stocktakes = new HashSet<Stocktake>();
            WriteOffCategories = new HashSet<WriteOffCategory>();
        }

        public long NLNominalAccountID { get; set; }
        public long NLAccountNumberCostCentreID { get; set; }
        public long NLDepartmentID { get; set; }
        public long? NLAccountReportCategoryID { get; set; }
        public long NLAccountTypeID { get; set; }
        public long NLCostCentreID { get; set; }
        public string ConsolidatedAccountNumber { get; set; }
        public string ConsolidatedCostCentre { get; set; }
        public string ConsolidatedDepartment { get; set; }
        public long? ParentNLNominalAccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountCostCentre { get; set; }
        public string AccountDepartment { get; set; }
        public int PeriodsToKeepTransactions { get; set; }
        public bool? AllowJournalsToBePosted { get; set; }
        public bool DisplayBalancesInSelectionList { get; set; }
        public bool PostBatchTotalsOnly { get; set; }
        public decimal CreditYearToDate { get; set; }
        public decimal DebitYearToDate { get; set; }
        public decimal BroughtForwardBalance { get; set; }
        public decimal ConsolidatedAdjustment { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public long? NLReportCategoryBudgetID { get; set; }
        public long? NLAccountSofaCategoryID { get; set; }
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
        public long SYSAccountStatusID { get; set; }
        public string StatusChangedBy { get; set; }
        public DateTime? StatusChangedDateTime { get; set; }

        public virtual NLAccountNumberCostCentre NLAccountNumberCostCentre { get; set; }
        public virtual NLAccountReportCategory NLAccountReportCategory { get; set; }
        public virtual NLAccountReportCategory NLAccountSofaCategory { get; set; }
        public virtual NLAccountType NLAccountType { get; set; }
        public virtual NLCostCentre NLCostCentre { get; set; }
        public virtual NLDepartment NLDepartment { get; set; }
        public virtual NLReportCategoryBudget NLReportCategoryBudget { get; set; }
        public virtual NLNominalAccount ParentNLNominalAccount { get; set; }
        public virtual SYSAccountStatus SYSAccountStatus { get; set; }
        public virtual ICollection<CBDirectDebitTran> CBDirectDebitTrans { get; set; }
        public virtual ICollection<InternalArea> InternalAreas { get; set; }
        public virtual ICollection<NLNominalAccount> InverseParentNLNominalAccount { get; set; }
        public virtual ICollection<MFGIssue> MFGIssueCreditNominals { get; set; }
        public virtual ICollection<MFGIssue> MFGIssueDebitNominals { get; set; }
        public virtual ICollection<NLAccountMemo> NLAccountMemos { get; set; }
        public virtual ICollection<NLAccountPeriodValue> NLAccountPeriodValues { get; set; }
        public virtual ICollection<NLAccountYearValue> NLAccountYearValues { get; set; }
        public virtual ICollection<NLCashFlowLayoutNominalAccount> NLCashFlowLayoutNominalAccounts { get; set; }
        public virtual ICollection<NLDefaultNominalAccount> NLDefaultNominalAccounts { get; set; }
        public virtual ICollection<NLPostedNominalTran> NLPostedNominalTrans { get; set; }
        public virtual ICollection<NLReconciliationEnquirySetting> NLReconciliationEnquirySettings { get; set; }
        public virtual ICollection<POPAdditionalCharge> POPAdditionalCharges { get; set; }
        public virtual ICollection<POPSetting> POPSettings { get; set; }
        public virtual ICollection<ProdGroupNominalCode> ProdGroupNominalCodes { get; set; }
        public virtual ICollection<SOPAdditionalCharge> SOPAdditionalCharges { get; set; }
        public virtual ICollection<SOPOrderReturnArch> SOPOrderReturnArches { get; set; }
        public virtual ICollection<SOPOrderReturn> SOPOrderReturns { get; set; }
        public virtual ICollection<SOPPaymentMethod> SOPPaymentMethods { get; set; }
        public virtual ICollection<StockItemNominalCode> StockItemNominalCodes { get; set; }
        public virtual ICollection<StockSetting> StockSettings { get; set; }
        public virtual ICollection<Stocktake> Stocktakes { get; set; }
        public virtual ICollection<WriteOffCategory> WriteOffCategories { get; set; }
    }
}