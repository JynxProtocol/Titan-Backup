﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class WarehouseItem
    {
        public WarehouseItem()
        {
            BinItems = new HashSet<BinItem>();
            POPOnOrderBalances = new HashSet<POPOnOrderBalance>();
            POPRequisitionFulfilmentLines = new HashSet<POPRequisitionFulfilmentLine>();
            POPStandardItemLinkArches = new HashSet<POPStandardItemLinkArch>();
            POPStandardItemLinks = new HashSet<POPStandardItemLink>();
            POPToReorderWarehouses = new HashSet<POPToReorderWarehouse>();
            SOPPreReceiptAllocs = new HashSet<SOPPreReceiptAlloc>();
            SOPStandardItemLinkArches = new HashSet<SOPStandardItemLinkArch>();
            SOPStandardItemLinks = new HashSet<SOPStandardItemLink>();
            StockPreReceiptAllocs = new HashSet<StockPreReceiptAlloc>();
        }

        public long WarehouseItemID { get; set; }
        public long WarehouseID { get; set; }
        public long ItemID { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal MinimumLevel { get; set; }
        public decimal MaximumLevel { get; set; }
        public DateTime? DateOfLastSale { get; set; }
        public decimal ConfirmedQtyInStock { get; set; }
        public decimal UnconfirmedQtyInStock { get; set; }
        public decimal QuantityAllocatedSOP { get; set; }
        public decimal QuantityAllocatedStock { get; set; }
        public decimal QuantityOnPOPOrder { get; set; }
        public decimal HoldingValueAtBuyPrice { get; set; }
        public DateTime? DateOfLastStockCount { get; set; }
        public decimal PreReceiptAllocationQty { get; set; }
        public decimal QuantityAllocatedBOM { get; set; }
        public decimal QuantityReserved { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual StockItem Item { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<BinItem> BinItems { get; set; }
        public virtual ICollection<POPOnOrderBalance> POPOnOrderBalances { get; set; }
        public virtual ICollection<POPRequisitionFulfilmentLine> POPRequisitionFulfilmentLines { get; set; }
        public virtual ICollection<POPStandardItemLinkArch> POPStandardItemLinkArches { get; set; }
        public virtual ICollection<POPStandardItemLink> POPStandardItemLinks { get; set; }
        public virtual ICollection<POPToReorderWarehouse> POPToReorderWarehouses { get; set; }
        public virtual ICollection<SOPPreReceiptAlloc> SOPPreReceiptAllocs { get; set; }
        public virtual ICollection<SOPStandardItemLinkArch> SOPStandardItemLinkArches { get; set; }
        public virtual ICollection<SOPStandardItemLink> SOPStandardItemLinks { get; set; }
        public virtual ICollection<StockPreReceiptAlloc> StockPreReceiptAllocs { get; set; }
    }
}