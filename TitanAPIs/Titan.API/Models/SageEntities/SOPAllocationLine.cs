// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SOPAllocationLine
    {
        public SOPAllocationLine()
        {
            SOPPreReceiptAllocs = new HashSet<SOPPreReceiptAlloc>();
        }

        public long SOPAllocationLineID { get; set; }
        public long SOPOrderReturnLineID { get; set; }
        public long AllocationID { get; set; }
        public string OrderNumber { get; set; }
        public string ItemCode { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime AllocatedDate { get; set; }
        public decimal AllocatedQuantity { get; set; }
        public decimal DespatchedQuantity { get; set; }
        public long PickingListPrintStatusID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal StockUnitAllocatedQuantity { get; set; }
        public decimal StockUnitDespatchedQuantity { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual AllocationBalance Allocation { get; set; }
        public virtual DocumentPrintStatus PickingListPrintStatus { get; set; }
        public virtual SOPOrderReturnLine SOPOrderReturnLine { get; set; }
        public virtual ICollection<SOPPreReceiptAlloc> SOPPreReceiptAllocs { get; set; }
    }
}