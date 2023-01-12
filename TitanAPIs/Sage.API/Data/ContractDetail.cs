//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sage.Api.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContractDetail
    {
        public int ConDetID { get; set; }
        public Nullable<int> ConID { get; set; }
        public Nullable<int> SageStkID { get; set; }
        public Nullable<long> WONumber { get; set; }
        public string DirtyStockCode { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public string WorkRequired { get; set; }
        public string Colour { get; set; }
        public string Warranty { get; set; }
        public Nullable<int> LeadTime { get; set; }
        public string DeliveryTerms { get; set; }
        public string DespatchMethod { get; set; }
        public Nullable<int> DeliveryDays { get; set; }
        public Nullable<int> QtyRequired { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> IsSerialised { get; set; }
        public Nullable<int> TotalQty { get; set; }
        public Nullable<int> TotalQtyReceived { get; set; }
        public string SpecialInstruction { get; set; }
        public string QuotationNumber { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> DateLastUpdated { get; set; }
        public Nullable<int> AlternativePart { get; set; }
        public Nullable<int> APTConID { get; set; }
        public string ParentPart { get; set; }
    }
}