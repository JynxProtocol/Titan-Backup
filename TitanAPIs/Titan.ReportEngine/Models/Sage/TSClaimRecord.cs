// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSClaimRecord
    {
        public long TSClaimRecordID { get; set; }
        public long? ParentProjectID { get; set; }
        public decimal Quantity { get; set; }
        public long? AuthoriseBehaviourID { get; set; }
        public long? RejectBehaviourID { get; set; }
        public long? TSCategoryGroupID { get; set; }
        public long TSClaimSheetID { get; set; }
        public DateTime DateIncurred { get; set; }
        public DateTime DateEntered { get; set; }
        public string Description { get; set; }
        public long TSReceiptAttachedTypeID { get; set; }
        public long TSClaimRecordStatusID { get; set; }
        public long SYSTaxCodeID { get; set; }
        public long? TSCategoryID { get; set; }
        public decimal UnitRate { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal TaxAmount { get; set; }
        public long? PCProjectEntryID { get; set; }
        public decimal OverheadUpliftRate { get; set; }
        public bool PostedToPayroll { get; set; }
        public DateTime? PostedToPayrollDate { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public long? ActivityID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual PCProjectEntry PCProjectEntry { get; set; }
        public virtual TSCategoryComponent TSCategory { get; set; }
        public virtual TSCategoryComponent TSCategoryGroup { get; set; }
        public virtual TSClaimRecordStatusType TSClaimRecordStatus { get; set; }
        public virtual TSClaimSheet TSClaimSheet { get; set; }
        public virtual TSReceiptAttachedType TSReceiptAttachedType { get; set; }
    }
}