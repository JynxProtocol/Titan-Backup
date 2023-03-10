// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MseStockItem
    {
        public MseStockItem()
        {
            BomBuildProducts = new HashSet<BomBuildProduct>();
        }

        public long MseStockItemID { get; set; }
        public string StockCode { get; set; }
        public int AggregateDays { get; set; }
        public decimal WorksOrderBatchMinQty { get; set; }
        public decimal WorksOrderBatchMaxQty { get; set; }
        public long? MseContactID { get; set; }
        public bool CanCancelWorksOrders { get; set; }
        public bool CanCancelPurchaseOrders { get; set; }
        public long MRPReplenishmentRulesTypeID { get; set; }
        public decimal MRPReplenishmentMultipleValue { get; set; }
        public bool CanAmendPOReceiptAllocation { get; set; }
        public bool Linked { get; set; }
        public bool UseDemandWarehouse { get; set; }
        public bool UseWOCompletionWarehouse { get; set; }
        public bool ApplyReorderLevelAfterMaximum { get; set; }
        public short ReplenishmentHorizonDays { get; set; }
        public bool BuiltBoughtDefaultMake { get; set; }
        public string AdditionalDescription2 { get; set; }
        public short LeadTime { get; set; }
        public decimal StdCost { get; set; }
        public bool Conversion { get; set; }
        public decimal BoughtInUnit { get; set; }
        public string BoughtInDesc { get; set; }
        public decimal ProcessUnit { get; set; }
        public string ProcessDesc { get; set; }
        public bool AutoUpdateSuppliers { get; set; }
        public bool Quarantine { get; set; }
        public decimal? MaximumStockLevel { get; set; }
        public int ShelfLifeNo { get; set; }
        public long? ShelfLifeInterval { get; set; }
        public decimal MaximumBatchSize { get; set; }
        public string BuyerCode { get; set; }
        public bool MakeItem { get; set; }
        public decimal OrderingMethod { get; set; }
        public decimal OrderMultiple { get; set; }
        public decimal ScrapPercent { get; set; }
        public bool StockConversionRound { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public bool BulkIssue { get; set; }
        public short BomItemType { get; set; }
        public string AdditionalDescription1 { get; set; }
        public bool CostFreeze { get; set; }
        public long MsmCostHeadingID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MseContact MseContact { get; set; }
        public virtual MsmCostHeading MsmCostHeading { get; set; }
        public virtual ICollection<BomBuildProduct> BomBuildProducts { get; set; }
    }
}