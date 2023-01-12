using System;

namespace Titan.Models.StockTake
{
    public class StockTakeDTO
    {
        public int ID { get; set; }
        public string Warehouse { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Completion { get; set; }
    }
}