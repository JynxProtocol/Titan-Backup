// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class MFGStockItem
    {
        public long MFGStockItemID { get; set; }
        public long StockItemID { get; set; }
        public int AggregateDays { get; set; }
        public decimal WorksOrderBatchMinQty { get; set; }
        public decimal WorksOrderBatchMaxQty { get; set; }
        public long? MFGContactID { get; set; }
        public bool? CanCancelWorksOrders { get; set; }
        public bool? CanCancelPurchaseOrders { get; set; }
        public long MRPReplenishmentRulesTypeID { get; set; }
        public decimal MRPReplenishmentMultipleValue { get; set; }
        public bool? CanAmendPOReceiptAllocation { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool Linked { get; set; }
        public bool? UseDemandWarehouse { get; set; }
        public bool? UseWOCompletionWarehouse { get; set; }
        public short ReplenishmentHorizonDays { get; set; }
        public bool? ApplyReorderLevelAfterMaximum { get; set; }
        public bool? BuiltBoughtDefaultMake { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MFGContact MFGContact { get; set; }
        public virtual MRPReplenishmentRulesType MRPReplenishmentRulesType { get; set; }
        public virtual StockItem StockItem { get; set; }
    }
}