// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BOM
    {
        public BOM()
        {
            BOMAllocFinishedItems = new HashSet<BOMAllocFinishedItem>();
            BOMBuildFinishedItems = new HashSet<BOMBuildFinishedItem>();
            BOMLines = new HashSet<BOMLine>();
        }

        public long BOMID { get; set; }
        public long StockItemID { get; set; }
        public string BOMVersion { get; set; }
        public string BOMShortName { get; set; }
        public string ChangeReference { get; set; }
        public bool IsOnHold { get; set; }
        public long? BOMReasonForHoldID { get; set; }
        public DateTime? HoldDate { get; set; }
        public long BOMStatusID { get; set; }
        public bool IsCostDirty { get; set; }
        public bool IsCheckedOut { get; set; }
        public long? BOMCheckedOutID { get; set; }
        public bool? AutoOverheadComputation { get; set; }
        public decimal OverheadCost { get; set; }
        public decimal PerUnitMaterialCost { get; set; }
        public decimal PerUnitExpenseCost { get; set; }
        public decimal AverageRunSizeForBatch { get; set; }
        public decimal PerBatchExpenseCost { get; set; }
        public decimal TotalCost { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastAmendedByUserName { get; set; }
        public DateTime LastAmendedOn { get; set; }
        public bool HasComponentReferences { get; set; }
        public DateTime LastAmendedTime { get; set; }
        public bool IsCurrentlyActive { get; set; }
        public bool IsMostRecent { get; set; }
        public string Code { get; set; }
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
        public DateTime DateTimeUpdated { get; set; }

        public virtual BOMCheckedOut BOMCheckedOut { get; set; }
        public virtual Old200BOMReasonForHold BOMReasonForHold { get; set; }
        public virtual BOMStatusType BOMStatus { get; set; }
        public virtual ICollection<BOMAllocFinishedItem> BOMAllocFinishedItems { get; set; }
        public virtual ICollection<BOMBuildFinishedItem> BOMBuildFinishedItems { get; set; }
        public virtual ICollection<BOMLine> BOMLines { get; set; }
    }
}