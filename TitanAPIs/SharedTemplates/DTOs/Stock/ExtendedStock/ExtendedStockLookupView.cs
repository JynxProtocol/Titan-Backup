using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Models.ExtendedStock
{
    public class ExtendedStockLookupView
    {
        public long ItemID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int LookupID { get; set; }
        public string ProductGroup { get; set; }
    }
}
