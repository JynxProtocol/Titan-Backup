namespace Titan.Models.StockTake
{
    public class StockAdjustDTO
    {
        public decimal SnapshotSageQuantity { get; set; }
        public decimal CurrentSageQuantity { get; set; }
        public decimal Change { get; set; }
        public decimal ResultSageQuantity { get; set; }
        public string Code { get; set; }
        public string Warehouse { get; set; } = "MAIN";
        public string BinName { get; set; }
        public string SecondRef { get; set; } = "Unknown user";
        public string Ref { get; set; } = "Add stock";
    }
}