using System;

namespace Titan.API.Models.AWK
{
    public class PartsListItem
    {
        public int PLDID { get; set; }
        public Nullable<int> PLHID { get; set; }
        public Nullable<int> SageStkID { get; set; }
        public int ItemNumber { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
        public string StockUnit { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturersPartNumber { get; set; }
        public bool IsAlternativePart { get; set; }
        public string AlternativePartTo { get; set; }
        public string PartsListID { get; set; }
        public double Qty { get; set; }
        public string WorkRequiredID { get; set; }
        public string AWKWork { get; set; }
        public string AWKFault { get; set; }
        public string RepairDetail { get; set; }
        public string ProductGroup { get; set; }
    }
}
