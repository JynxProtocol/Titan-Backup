using System.Collections.Generic;

namespace Titan.Models.StockTake
{
    public class AdjustResponseReportDTO
    {
        public List<AdjustResponseDTO> Report { get; set; }
        public bool Success { get; set; }
        public bool StockTakeClosed { get; set; }
    }
}
