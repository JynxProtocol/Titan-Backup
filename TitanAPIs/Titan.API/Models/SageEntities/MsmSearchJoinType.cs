﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class MsmSearchJoinType
    {
        public MsmSearchJoinType()
        {
            MsmSearchLines = new HashSet<MsmSearchLine>();
        }

        /// <summary>
        /// Primary Key
        /// </summary>
        public long MsmSearchJoinTypeID { get; set; }
        /// <summary>
        /// The name of the join
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<MsmSearchLine> MsmSearchLines { get; set; }
    }
}