﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepWorkToOperation
    {
        public long OpID { get; set; }
        public long? WoID { get; set; }
        public string StageReference { get; set; }
        public long? StageID { get; set; }
        public double? Sequence { get; set; }
        public string OperationReference { get; set; }
        public string OperationDescription { get; set; }
        public decimal? QtyOs { get; set; }
        public string SetupStart { get; set; }
        public string StartDate { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string EndDate { get; set; }
        public string MachineReference { get; set; }
        public string MachineDescription { get; set; }
        public string LabourReference { get; set; }
        public string LabourDescription { get; set; }
    }
}