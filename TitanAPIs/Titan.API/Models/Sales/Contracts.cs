using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Titan.API.Models.Sales
{
    public class ContractOverview
    {
        public int conID { get; set; }
        public long ConCusID { get; set; }
        public string ContractName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerOrderNumber { get; set; }
        public int DeliveriesReceived { get; set; }
        public int Active { get; set; }
        public decimal TotalValue { get; set; }
        public decimal RemainingValue { get; set; }
        public int Acknowledged { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class Contract
    {
        public int ConID { get; set; }
        public long ConCusID { get; set; }

        public string CustomerName { get; set; }

        public string ContractName { get; set; }

        public string CustomerOrderNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> CustomerOrderDate { get; set; }


        public string PostalName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string BookingInInstructions { get; set; }
        public bool IsCasualty { get; set; }
        public string DeliveryTerms { get; set; }
        public string DespatchMethod { get; set; }

        public List<ContractItem> ContractItems { get; set; }
        public List<ContractDocuments> ContractDocuments { get; set; }
    }

    public class ContractDocuments
    {
        public int DocID { get; set; }
        public int DocumentType { get; set; }
        public int ConID { get; set; }
        public string DocumentName { get; set; }
        public string FilePath { get; set; }
    }

    public class ContractItem
    {
        public int ConDetID { get; set; }
        public Nullable<int> ConID { get; set; }
        public int StkID { get; set; }
        public Nullable<int> SageStkID { get; set; }
        public string WONumber { get; set; }
        public string DirtyStockCode { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public string WorkRequired { get; set; } = "Overhaul Test & Return";
        public string Colour { get; set; }
        public string Warranty { get; set; }
        public Nullable<int> LeadTime { get; set; }
        public string DeliveryTerms { get; set; } = "Ex Works";
        public string DespatchMethod { get; set; }
        public Nullable<int> DeliveryDays { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        public int QtyRequired { get; set; }

        public int QtyOutstanding { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        public Nullable<int> TotalQty { get; set; }

        public Nullable<int> TotalQtyReceived { get; set; }
        public string SpecialInstruction { get; set; }
        public string QuotationNumber { get; set; }
        public int IsSerialised { get; set; }
        public byte[] Image { get; set; }
        public Nullable<int> AltPart { get; set; }
        public Nullable<int> AltTo { get; set; }
        public string Parent { get; set; }
    }
}