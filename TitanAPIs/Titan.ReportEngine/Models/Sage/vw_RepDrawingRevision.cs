// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class vw_RepDrawingRevision
    {
        public long ID { get; set; }
        public long? DrawingID { get; set; }
        public string RevisionNumber { get; set; }
        public string Revision { get; set; }
        public DateTime? RevisionDate { get; set; }
    }
}