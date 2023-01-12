using System;

namespace Titan.API.Models.AWK
{
    public class AWKHoldNote
    {
        public int HNID { get; set; }
        public string WONumber { get; set; }
        public string CatNo { get; set; }
        public string Description { get; set; }
        public int AWKQty { get; set; }
        public string Fault { get; set; }
        public string SOR { get; set; }
        public string AccountName { get; set; }
        public string RaisedBy { get; set; }
        public DateTime DateRasied { get; set; }
    }
}
