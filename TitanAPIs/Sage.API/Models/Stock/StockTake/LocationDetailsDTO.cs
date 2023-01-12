using System.Collections.Generic;

namespace Titan.Models.StockTake
{
    public class LocationDetailsDTO
    {
        public List<string> Locations { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
