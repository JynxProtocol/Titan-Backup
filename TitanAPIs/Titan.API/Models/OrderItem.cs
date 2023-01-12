namespace Titan.API.Models
{
    public class OrderItem
    {
        public int DetID { get; set; }
        public int? OrdID { get; set; }
        public int? ConDetID { get; set; }
        public int? SageStkID { get; set; }
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
        public int? Qty { get; set; }
        public int? QtyReceived { get; set; }
        public int? ExpectedQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? IsCasualty { get; set; }
        public string Location { get; set; }
        public string SerialNumber { get; set; }
        public string SpecialInstruction { get; set; }
        public string QuotationNumber { get; set; }
        public int? QtyOutstanding { get; set; }
        public string AssetNumber { get; set; }
    }
}
