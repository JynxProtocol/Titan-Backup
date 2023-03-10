// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class StockItemSupplier
    {
        public long StockItemSupplierID { get; set; }
        public long ItemID { get; set; }
        public long SupplierID { get; set; }
        public string SupplierStockCode { get; set; }
        public short LeadTime { get; set; }
        public long? LeadTimeUnitID { get; set; }
        public decimal UsualOrderQuantity { get; set; }
        public decimal MinimumOrderQuantity { get; set; }
        public bool Preferred { get; set; }
        public DateTime? DateLastOrder { get; set; }
        public decimal LastBuyingPrice { get; set; }
        public decimal LastOrderQuantity { get; set; }
        public decimal OrderQuantityYTD { get; set; }
        public decimal OrderValueYTD { get; set; }
        public decimal QuantityOnOrder { get; set; }
        public decimal LastBaseBuyingPrice { get; set; }
        public decimal CataloguePrice { get; set; }
        public DateTime? CataloguePriceDate { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal ListPrice { get; set; }
        public decimal ListBasePrice { get; set; }
        public DateTime? DateListPriceChanged { get; set; }
        public DateTime? ListPriceExpiryDate { get; set; }
        public long DefaultPricingSourceTypeID { get; set; }
        public long LandedCostsTypeID { get; set; }
        public decimal LandedCostsValue { get; set; }
        public decimal ReorderMultipleQty { get; set; }
        public long? CountryOfOriginID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSCountryCode CountryOfOrigin { get; set; }
        public virtual DefaultPricingSourceType DefaultPricingSourceType { get; set; }
        public virtual StockItem Item { get; set; }
        public virtual LandedCostsType LandedCostsType { get; set; }
        public virtual TimeUnit LeadTimeUnit { get; set; }
        public virtual PLSupplierAccount Supplier { get; set; }
    }
}