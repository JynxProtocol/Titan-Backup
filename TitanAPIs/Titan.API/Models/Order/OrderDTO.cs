using System;
using System.Collections.Generic;

namespace Titan.API.Models.DGRN
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int? ConID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateReceived { get; set; }
        public string CusOrderNumber { get; set; }
        public string CustomerName { get; set; }
        public int? CusID { get; set; }
        public string OrdCustomerRef { get; set; }
        public string OrdSpecialInstruction { get; set; }
        public string PostalName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string DeliveryTerms { get; set; }
        public string DespatchMethod { get; set; }
        public bool Approved { get; set; }
        public string ApprovedBy { get; set; }
        public string SOR { get; set; }
        public string GRN { get; set; }

        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}
