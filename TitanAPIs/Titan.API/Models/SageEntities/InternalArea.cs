﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class InternalArea
    {
        public long InternalAreaID { get; set; }
        public string InternalAreaName { get; set; }
        public bool ThisIsDefault { get; set; }
        public long NominalCodeID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLNominalAccount NominalCode { get; set; }
    }
}