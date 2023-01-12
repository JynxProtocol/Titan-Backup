﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SOPPaymentArch
    {
        public SOPPaymentArch()
        {
            SOPPaymentInvCredLineArches = new HashSet<SOPPaymentInvCredLineArch>();
        }

        public long SOPPaymentID { get; set; }
        public long SOPOrderReturnID { get; set; }
        public decimal LineTotalValue { get; set; }
        public decimal LineTaxValue { get; set; }
        public long PaymentInvoiceStatusID { get; set; }
        public long? TaxCodeID { get; set; }
        public string NominalAccountRef { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public string Description { get; set; }

        public virtual PaymentInvoiceStatus PaymentInvoiceStatus { get; set; }
        public virtual SOPOrderReturnArch SOPOrderReturn { get; set; }
        public virtual SYSTaxRate TaxCode { get; set; }
        public virtual ICollection<SOPPaymentInvCredLineArch> SOPPaymentInvCredLineArches { get; set; }
    }
}