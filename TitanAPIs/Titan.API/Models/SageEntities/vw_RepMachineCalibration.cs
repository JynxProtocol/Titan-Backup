﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class vw_RepMachineCalibration
    {
        public long? MachineID { get; set; }
        public DateTime? LastCalibration { get; set; }
        public string Result { get; set; }
        public string Repairs { get; set; }
        public string Notes { get; set; }
        public DateTime? NextCalibration { get; set; }
    }
}