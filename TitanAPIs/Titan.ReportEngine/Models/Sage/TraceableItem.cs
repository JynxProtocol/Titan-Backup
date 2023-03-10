// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TraceableItem
    {
        public TraceableItem()
        {
            MFGComponentTraceabilities = new HashSet<MFGComponentTraceability>();
            MFGTraceableBuiltItems = new HashSet<MFGTraceableBuiltItem>();
            STKTraceItemBatchAttributes = new HashSet<STKTraceItemBatchAttribute>();
            TraceableBinItems = new HashSet<TraceableBinItem>();
        }

        public long TraceableItemID { get; set; }
        public string IdentificationNo { get; set; }
        public long UniqueDuplicateNo { get; set; }
        public string AdditionalReference { get; set; }
        public DateTime? SellByDate { get; set; }
        public DateTime? UseByDate { get; set; }
        public long TraceableItemStatusID { get; set; }
        public long StockItemID { get; set; }
        public decimal GoodsInQuantity { get; set; }
        public decimal GoodsOutQuantity { get; set; }
        public decimal AllocatedQuantity { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string SupplierGRNNo { get; set; }
        public long TraceableTypeID { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public long? SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierRef { get; set; }
        public long SourceAreaTypeID { get; set; }
        public string Barcode { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public decimal QuantityReserved { get; set; }
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
        public bool LabelPrinted { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SourceAreaType SourceAreaType { get; set; }
        public virtual StockItem StockItem { get; set; }
        public virtual TraceableItemStatus TraceableItemStatus { get; set; }
        public virtual TraceableType TraceableType { get; set; }
        public virtual ICollection<MFGComponentTraceability> MFGComponentTraceabilities { get; set; }
        public virtual ICollection<MFGTraceableBuiltItem> MFGTraceableBuiltItems { get; set; }
        public virtual ICollection<STKTraceItemBatchAttribute> STKTraceItemBatchAttributes { get; set; }
        public virtual ICollection<TraceableBinItem> TraceableBinItems { get; set; }
    }
}