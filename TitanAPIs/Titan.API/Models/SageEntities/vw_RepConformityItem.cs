﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepConformityItem
    {
        public long ID { get; set; }
        public long? CertID { get; set; }
        public long? SONumber { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? LineTotal { get; set; }
        public string Comment_1 { get; set; }
        public string Comment_2 { get; set; }
        public string Units { get; set; }
    }
}