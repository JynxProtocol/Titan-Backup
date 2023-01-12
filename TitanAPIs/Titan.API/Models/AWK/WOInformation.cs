using System;
using System.Collections.Generic;

namespace Titan.API.Models.AWK
{
    public class WOInformation
    {
        public int ID { get; set; }
        public string WorksOrderNumber { get; set; }
        public string BomReference { get; set; }
        public string BomDescription { get; set; }
        public decimal QtyRequired { get; set; }
        public string LinkedTo { get; set; } //SOR - (LinkedTo) - SalesOrderNumber (Sage Number)
        public string CusOrderNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AWKFault { get; set; }
        public string WorkRequired { get; set; }
        public string Warranty { get; set; }
        public string Colour { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string AWKWork { get; set; } = "Replace Parts";

        public List<PartsListItem> PartsListItems { get; set; }
    }
}
