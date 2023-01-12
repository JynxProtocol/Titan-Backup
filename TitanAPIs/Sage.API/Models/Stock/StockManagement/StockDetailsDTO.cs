using System.Collections.Generic;

namespace Titan.Models.StockManagement
{
    public class StockDetailsDTO
    {
        public string Code { get; set; }
        public decimal Quantity { get; set; }
        public decimal KanbanQuantity { get; set; }
        public bool IsKanban { get; set; } = false;
        public bool IsEnoughStockToIssue { get; set; } = true;
        public string Description { get; set; }
        public string Name { get; set; }
        public IEnumerable<BinLocationDTO> Locations { get; set; }
        public IEnumerable<string> InternalAreas { get; set; }
        public BinLocationDTO BestLocation { get; set; }
    }
}
