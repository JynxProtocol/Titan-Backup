// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.API.Models
{
    public partial class GRNLocation
    {
        public int GRNID { get; set; }
        public int? OrdID { get; set; }
        public int? DetID { get; set; }
        public string Location { get; set; }
        public int? Qty { get; set; }
        public int? QtyIssued { get; set; }
        public DateTime? DateIssued { get; set; }
    }
}