﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.ReportEngine.Models.Titan
{
    public partial class PartsListCat
    {
        public int CATID { get; set; }
        public int? PLHID { get; set; }
        public string CatNumber { get; set; }
        public string Description { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public int? SageStkID { get; set; }
    }
}