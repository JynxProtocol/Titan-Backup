// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class POPReceiptReturnType
    {
        public POPReceiptReturnType()
        {
            POPReceiptReturns = new HashSet<POPReceiptReturn>();
        }

        public long POPReceiptReturnTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POPReceiptReturn> POPReceiptReturns { get; set; }
    }
}