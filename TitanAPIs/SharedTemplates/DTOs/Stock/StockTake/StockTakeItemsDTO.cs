using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Titan.Models.StockTake
{
    public class StockTakeItemsDTO
    {
        public string Code { get; set; }
        public string Description { get; set; }
        [DataType("date")] 
        public DateTime? DateOfLastStockCount { get; set; }
        public int QuantityAllocated { get; set; }
        public string ProductGroup { get; set; }
        public bool Selected { get; set; } = false;
    }
}
