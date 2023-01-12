using System;

namespace Titan.API.Models.DGRN
{
    public class OrderDetailDTO
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int? ConDetID { get; set; }
        public int? ExpectedQty { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? DetSODID { get; set; }
        public string WONumber { get; set; }
        public string DirtyStockCode { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public string WorkRequired { get; set; }
        public string Colour { get; set; }
        public string Warranty { get; set; }
        public int? LeadTime { get; set; }
        public string DeliveryTerms { get; set; }
        public string DespatchMethod { get; set; }
        public int? DeliveryDays { get; set; }
        public int? QtyReceived { get; set; }
        public decimal? UnitPrice { get; set; }
        public string SpecialInstruction { get; set; }
        public string SerialNumber { get; set; }
    }
}
