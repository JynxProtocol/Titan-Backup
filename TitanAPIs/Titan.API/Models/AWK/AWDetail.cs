namespace Titan.API.Models.AWK
{
    public class AWDetail
    {
        public int AWDID { get; set; }
        public int DID { get; set; }
        public int ID { get; set; }
        public int StkID { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public int RequiredQty { get; set; }
        public string Fault { get; set; }
        public string WorkRequired { get; set; } = "Replace parts";
        public string RepairDetail { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal FreeStock { get; set; }
        public decimal FreeSabreStock { get; set; }
        public decimal QtyonOrder { get; set; }
        public decimal RepairCost { get; set; }
        public decimal Linetotal { get; set; }
        public int AQ { get; set; }
        public bool SabreStock { get; set; }
        public bool ComAccepted { get; set; }
    }
}
