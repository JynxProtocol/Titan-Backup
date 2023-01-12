using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Titan.API.Models.AWK
{
    public class AWHeader
    {
        public int ID { get; set; }
        public string WorksOrderNumber { get; set; }
        public string BomReference { get; set; }
        public string BomDescription { get; set; }
        public int QtyRequired { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Cannot authorise AWK for less than {1} items")]
        public int NumberOfConsolidatedAffectedItems { get; set; }
        public string LinkedTo { get; set; } //SOR - (LinkedTo) - SalesOrderNumber (Sage Number)
        public string CusOrderNumber { get; set; }
        public string AccountName { get; set; }

        public string ContractName { get; set; }
        public string EMail { get; set; }

        public string AWKFault { get; set; }

        public string AWKWork { get; set; }

        public List<AWDetail> AWDetails { get; set; }
        public List<Document> Documents { get; set; }
        public string EmailMessage { get; set; }

        public string AWKSalesType { get; set; }

        public bool SendMail { get; set; }

        public DateTime QuotationDate { get; set; }

        public string QuotedBy { get; set; }
        public bool AWKQuoted { get; set; }
        public bool AWKApproved { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
    }
}
