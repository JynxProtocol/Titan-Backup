﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class POPToReorderItemStatus
    {
        public POPToReorderItemStatus()
        {
            POPToReorderItems = new HashSet<POPToReorderItem>();
        }

        public long POPToReorderItemStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POPToReorderItem> POPToReorderItems { get; set; }
    }
}