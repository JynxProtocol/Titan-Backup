﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class MsmDesktopElementType
    {
        public MsmDesktopElementType()
        {
            MsmDesktopElements = new HashSet<MsmDesktopElement>();
        }

        /// <summary>
        /// Primary Key
        /// </summary>
        public long MsmDesktopElementTypeID { get; set; }
        /// <summary>
        /// Element Type.  e.g. Toolbar Item, Menu Item etc.
        /// </summary>
        public string Description { get; set; }

        public virtual ICollection<MsmDesktopElement> MsmDesktopElements { get; set; }
    }
}