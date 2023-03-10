// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SOPStandardItemLink
    {
        public long SOPStandardItemLinkID { get; set; }
        public long SOPOrderReturnLineID { get; set; }
        public long ItemID { get; set; }
        public long? WarehouseItemID { get; set; }
        public long SellingUnitID { get; set; }
        public long SellingPriceUnitID { get; set; }
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

        public virtual StockItem Item { get; set; }
        public virtual SOPOrderReturnLine SOPOrderReturnLine { get; set; }
        public virtual StockItemUnit SellingPriceUnit { get; set; }
        public virtual StockItemUnit SellingUnit { get; set; }
        public virtual WarehouseItem WarehouseItem { get; set; }
    }
}