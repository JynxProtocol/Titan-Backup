using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Models.StockManagement
{
    public class StockResponseDTO
    {
        public bool Success { get; set; } = false;
        public bool Changed { get; set; } = false;
        public string Message { get; set; }
    }
}
