using System;
using System.Collections.Generic;

namespace Titan.API.Models
{
    public class Order
    {
        public int OrdID { get; set; }
        public int ConID { get; set; }
        public string CustomerName { get; set; }
        public string CusOrderNumber { get; set; }
        public int CusID { get; set; }
        public long? SOR { get; set; }
        public DateTime? DateReceived { get; set; }
        public string Username { get; set; }
        public int? Authorised { get; set; }
        public int? Status { get; set; }
        public int? BatchCount { get; set; }
        public string GRN { get; set; }
        public int? OrdIsCasualty { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string PostalName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string DeliveryTerms { get; set; }
        public string DespatchMethod { get; set; }

    }
}
