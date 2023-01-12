﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class MachMachineResourceActivity
    {
        public long MachMachineResourceActivityID { get; set; }
        public long MachMachineResourceID { get; set; }
        public long MachMachineResourceActivityTypeID { get; set; }
        public string Reference { get; set; }
        public DateTime ThisActivityDate { get; set; }
        public DateTime? NextActivityDate { get; set; }
        public bool Archived { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public string Result { get; set; }
        public string Notes { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MachMachineResource MachMachineResource { get; set; }
        public virtual MachMachineResourceActivityType MachMachineResourceActivityType { get; set; }
    }
}