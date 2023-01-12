using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Models.StockTake
{
    public class AdjustResponseReportDTO
    {
        public List<AdjustResponseDTO> Report { get; set; }
        public bool Success { get; set; }
        public bool StockTakeClosed { get; set; }
    }
}
