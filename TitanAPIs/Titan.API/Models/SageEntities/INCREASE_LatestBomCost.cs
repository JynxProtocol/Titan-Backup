// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class INCREASE_LatestBomCost
    {
        public string BomReference { get; set; }
        public string BomVersion { get; set; }
        public decimal QtyCosted { get; set; }
        public string CostHeadingName { get; set; }
        public decimal? TotalCost { get; set; }
        public long BomRecordID { get; set; }
    }
}