namespace Titan.Models.StockManagement
{
    public class IssueStockDTO
    {
        public decimal Quantity { get; set; }
        public string Warehouse { get; set; } = "MAIN";
        public string BinName { get; set; } = "Unspecified"; //We need to find and specify the bin Name been Issued From
        public string InternalArea { get; set; } = "Distributors"; //We need to specifiy this expliclitly in the send not the DTO
        public string SecondRef { get; set; } = "Unknown user";
        public string Ref { get; set; } = "Kanban Issue";
    }
}

