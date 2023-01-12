﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SubContractingDespatch
    {
        public long ID { get; set; }
        public string Source { get; set; }
        public long OpID { get; set; }
        public decimal Quantity { get; set; }
        public DateTime DespatchDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string AdviceNoteNumber { get; set; }
        public string SupplierRef { get; set; }
        public string Name { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string Address_3 { get; set; }
        public string Address_4 { get; set; }
        public string Address_5 { get; set; }
        public string ContactName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressPostcode { get; set; }
        public string AddressCity { get; set; }
        public string AddressCounty { get; set; }
        public long? AddressCountryID { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactMiddleName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPreMigratedData { get; set; }
        public long ContactSalutationID { get; set; }

        public virtual SYSCountryCode AddressCountry { get; set; }
        public virtual Salutation ContactSalutation { get; set; }
    }
}