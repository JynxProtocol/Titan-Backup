using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Titan.Models.StockTake
{
    public class StockTakeInfoDTO : StockTakeDTO
    {
        public List<StockTakeItemInfoDTO> Items { get; set; } = new List<StockTakeItemInfoDTO>();
    }
}