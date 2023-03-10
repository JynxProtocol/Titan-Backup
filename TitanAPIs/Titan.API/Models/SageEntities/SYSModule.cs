// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSModule
    {
        public SYSModule()
        {
            NLDeferredNominalTranNominalPeriodModuleNavigations = new HashSet<NLDeferredNominalTran>();
            NLDeferredNominalTranSourceNavigations = new HashSet<NLDeferredNominalTran>();
            NLPendNominalTranNominalPeriodModuleNavigations = new HashSet<NLPendNominalTran>();
            NLPendNominalTranSourceNavigations = new HashSet<NLPendNominalTran>();
            PCIntegrationOptions = new HashSet<PCIntegrationOption>();
            PLPendSupplierTrans = new HashSet<PLPendSupplierTran>();
            POPAuthPrincipalContents = new HashSet<POPAuthPrincipalContent>();
            SYSAccountingModulePermisses = new HashSet<SYSAccountingModulePermiss>();
            SYSDocumentTypes = new HashSet<SYSDocumentType>();
            SYSModuleAccountingPeriods = new HashSet<SYSModuleAccountingPeriod>();
            SYSPeriodBalancesLedgers = new HashSet<SYSPeriodBalancesLedger>();
            TSClaimSheets = new HashSet<TSClaimSheet>();
        }

        public long SYSModuleID { get; set; }
        public string Name { get; set; }
        public DateTime? DateArchiveLastRun { get; set; }
        public string ArchiveLastRunBy { get; set; }
        public DateTime? DateBroughtForwardMaintLastRun { get; set; }
        public string BroughtForwardMaintLastRunBy { get; set; }
        public DateTime? DateAutoAllocationLastRun { get; set; }
        public string AutoAllocationLastRunBy { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public string ShortName { get; set; }
        public DateTime? DateTansactionRemovalLastRun { get; set; }
        public string TransactionRemovalLastRunBy { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<NLDeferredNominalTran> NLDeferredNominalTranNominalPeriodModuleNavigations { get; set; }
        public virtual ICollection<NLDeferredNominalTran> NLDeferredNominalTranSourceNavigations { get; set; }
        public virtual ICollection<NLPendNominalTran> NLPendNominalTranNominalPeriodModuleNavigations { get; set; }
        public virtual ICollection<NLPendNominalTran> NLPendNominalTranSourceNavigations { get; set; }
        public virtual ICollection<PCIntegrationOption> PCIntegrationOptions { get; set; }
        public virtual ICollection<PLPendSupplierTran> PLPendSupplierTrans { get; set; }
        public virtual ICollection<POPAuthPrincipalContent> POPAuthPrincipalContents { get; set; }
        public virtual ICollection<SYSAccountingModulePermiss> SYSAccountingModulePermisses { get; set; }
        public virtual ICollection<SYSDocumentType> SYSDocumentTypes { get; set; }
        public virtual ICollection<SYSModuleAccountingPeriod> SYSModuleAccountingPeriods { get; set; }
        public virtual ICollection<SYSPeriodBalancesLedger> SYSPeriodBalancesLedgers { get; set; }
        public virtual ICollection<TSClaimSheet> TSClaimSheets { get; set; }
    }
}