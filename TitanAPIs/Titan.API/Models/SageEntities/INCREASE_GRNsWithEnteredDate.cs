﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class INCREASE_GRNsWithEnteredDate
    {
        public string Stock_Code { get; set; }
        public string GRN_No_ { get; set; }
        public string Order_No_ { get; set; }
        public DateTime? GRN_Date { get; set; }
        public DateTime Date_Entered { get; set; }
        public string Entered_By { get; set; }
    }
}