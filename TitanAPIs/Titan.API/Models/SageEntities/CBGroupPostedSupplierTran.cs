﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CBGroupPostedSupplierTran
    {
        public long CBGroupPostedSupplierTranID { get; set; }
        public long CBPostedAccountTranID { get; set; }
        public long PLPostedSupplierTranID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual CBPostedAccountTran CBPostedAccountTran { get; set; }
        public virtual PLPostedSupplierTran PLPostedSupplierTran { get; set; }
    }
}