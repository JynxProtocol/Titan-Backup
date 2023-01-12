namespace Titan.Models.StockManagement
{
    public class StockResponseDTO
    {
        public bool Success { get; set; } = false;
        public bool Changed { get; set; } = false;
        public string Message { get; set; }
    }

    public class TraceableStockResponseDTO : StockResponseDTO
    {
        public string BatchNo { get; set; }
    }
}
