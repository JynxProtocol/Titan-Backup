// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.API.Models
{
    public partial class TrackingOperation
    {
        public TrackingOperation()
        {
            TrackingDetails = new HashSet<TrackingDetail>();
            TrackingHeaders = new HashSet<TrackingHeader>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TrackingDetail> TrackingDetails { get; set; }
        public virtual ICollection<TrackingHeader> TrackingHeaders { get; set; }
    }
}