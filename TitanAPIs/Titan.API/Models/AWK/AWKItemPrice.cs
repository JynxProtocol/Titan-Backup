using System;

namespace Titan.API.Models.AWK
{
    public class AWKItemPrice
    {
        public int StkID { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? DateLastUpdated { get; set; }
    }
}
