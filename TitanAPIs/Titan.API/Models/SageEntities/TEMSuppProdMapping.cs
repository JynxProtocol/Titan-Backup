// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TEMSuppProdMapping
    {
        public long TEMSuppProdMappingID { get; set; }
        public long? SupplierID { get; set; }
        public long? StockItemID { get; set; }
        public string SupplierProductCode { get; set; }
        public string SupplierProductDesc { get; set; }
        public long? AdditionalChargeID { get; set; }
        public string AdditionalChargeCode { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual StockItem StockItem { get; set; }
        public virtual PLSupplierAccount Supplier { get; set; }
    }
}