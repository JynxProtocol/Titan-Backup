﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_WoWorkInProgress
    {
        public long WoID { get; set; }
        public string WorksOrderNumber { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal IssueQty { get; set; }
        public decimal? IssueCost { get; set; }
        public decimal? UnitCost { get; set; }
        public int OneOff { get; set; }
        public string AnalysisCode_1 { get; set; }
        public string AnalysisCode_2 { get; set; }
        public string AnalysisCode_3 { get; set; }
        public string AnalysisCode_4 { get; set; }
        public string AnalysisCode_5 { get; set; }
    }
}