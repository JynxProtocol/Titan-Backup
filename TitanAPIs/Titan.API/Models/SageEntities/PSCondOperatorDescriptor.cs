﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PSCondOperatorDescriptor
    {
        public PSCondOperatorDescriptor()
        {
            PSExpressionLines = new HashSet<PSExpressionLine>();
        }

        public long PSCondOperatorDescriptorID { get; set; }
        public long PSCondOperatorTypeID { get; set; }

        public virtual PSCondOperatorType PSCondOperatorType { get; set; }
        public virtual ICollection<PSExpressionLine> PSExpressionLines { get; set; }
    }
}