﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class BomVersionsImage
    {
        public long ID { get; set; }
        public long? HeaderID { get; set; }
        public byte[] Picture { get; set; }

        public virtual BomVersionsHeader Header { get; set; }
    }
}