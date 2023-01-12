using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Models.StockTake
{
    public class AdjustResponseDTO
    {
        public string Code { get; set; }
        public string Bin { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
