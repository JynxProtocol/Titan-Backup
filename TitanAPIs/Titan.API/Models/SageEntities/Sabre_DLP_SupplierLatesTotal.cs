// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class Sabre_DLP_SupplierLatesTotal
    {
        public string SupplierAccountName { get; set; }
        public int? LateDeliveries { get; set; }
        public int? TotalDeliverysForCurrentYear { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public DateTime Date { get; set; }
    }
}