using System;

namespace Titan.Models.StockTake
{
    public class StockTakeItemInfoDTO
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal? FreeStock { get; set; }
        public decimal? RecordedValue { get; set; }
        public decimal? Adjustment { get; set; }
        public decimal? AdjustmentValue { get; set; }
        public string RecordedBy { get; set; }
        public DateTime? RecordedDate { get; set; }
    }
}