﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SOPInvCredNominalItem
    {
        public long SOPInvCredNominalItemID { get; set; }
        public long SOPInvoiceCreditID { get; set; }
        public string NominalAccountRef { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public decimal NominalValue { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SOPInvoiceCredit SOPInvoiceCredit { get; set; }
    }
}