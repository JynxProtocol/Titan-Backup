namespace Titan.Models.StockManagement
{
    public class AddStockDTO
    {
        public decimal Quantity { get; set; }
        public string Warehouse { get; set; } = "MAIN";
        public string BinName { get; set; } = "Unspecified";
        public string SecondRef { get; set; } = "Unknown user";
        public string Ref { get; set; } = "Add stock";
    }
}
