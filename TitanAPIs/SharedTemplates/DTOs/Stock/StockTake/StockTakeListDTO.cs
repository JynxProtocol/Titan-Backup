using System.Collections.Generic;

namespace Titan.Models.StockTake
{
    public class StockTakeListDTO
    {
        public List<StockTakeDTO> Active { get; set; }
        public List<StockTakeDTO> Complete { get; set; }
        public List<StockTakeDTO> Deleted { get; set; }

    }
}