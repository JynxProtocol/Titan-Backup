﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SABRE_DLP_SalesOrderBook_V2
    {
        public string CustomerAccountName { get; set; }
        public string DocumentNo { get; set; }
        public string CustomerDocumentNo { get; set; }
        public DateTime? PromisedDeliveryDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string PickingListComment { get; set; }
        public decimal LineQuantity { get; set; }
        public decimal AllocatedQuantity { get; set; }
        public decimal DespatchReceiptQuantity { get; set; }
        public string PostCode { get; set; }
        public string Code { get; set; }
    }
}