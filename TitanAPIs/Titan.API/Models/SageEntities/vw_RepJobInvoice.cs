// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepJobInvoice
    {
        public long ID { get; set; }
        public long? StageID { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
        public bool Posted { get; set; }
        public decimal? Total { get; set; }
    }
}