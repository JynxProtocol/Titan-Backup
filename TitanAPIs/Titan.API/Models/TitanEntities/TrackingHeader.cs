﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.API.Models
{
    public partial class TrackingHeader
    {
        public TrackingHeader()
        {
            TrackingDetails = new HashSet<TrackingDetail>();
        }

        public long WONumber { get; set; }
        public int? CurrentOperationId { get; set; }
        public bool? IsFinished { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }

        public virtual TrackingOperation CurrentOperation { get; set; }
        public virtual ICollection<TrackingDetail> TrackingDetails { get; set; }
    }
}