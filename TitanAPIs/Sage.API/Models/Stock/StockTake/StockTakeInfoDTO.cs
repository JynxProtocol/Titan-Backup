using System.Collections.Generic;

namespace Titan.Models.StockTake
{
    public class StockTakeInfoDTO : StockTakeDTO
    {
        public List<StockTakeItemInfoDTO> Items { get; set; } = new List<StockTakeItemInfoDTO>();
    }
}