﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSCondOperatorType
    {
        public SYSCondOperatorType()
        {
            SYSCondOperatorDescriptors = new HashSet<SYSCondOperatorDescriptor>();
        }

        public long SYSCondOperatorTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SYSCondOperatorDescriptor> SYSCondOperatorDescriptors { get; set; }
    }
}