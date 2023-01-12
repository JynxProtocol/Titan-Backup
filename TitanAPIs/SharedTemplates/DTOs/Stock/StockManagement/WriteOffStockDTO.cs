using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Models.StockManagement
{
    public class WriteOffStockDTO
    {
        public decimal Quantity { get; set; }
        public string Warehouse { get; set; } = "MAIN";
        public string BinName { get; set; } = "Unspecified";
        public string SecondRef { get; set; } = "Unknown user";
        public string Ref { get; set; } = "Write off stock";
    }
}
