namespace Titan.Models.StockManagement
{
    public class StockCorrectDTO
    {
        public decimal Amount { get; set; }

        public string Warehouse { get; set; } = "MAIN";
        public string BinName { get; set; } = "Unspecified";
        public string InternalArea { get; set; }
        public string SecondRef { get; set; } = "Unknown user";
        public string Ref { get; set; } = "Stock correction";
    }
}
