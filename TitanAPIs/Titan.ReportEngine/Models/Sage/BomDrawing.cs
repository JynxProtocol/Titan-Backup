// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BomDrawing
    {
        public long BomDrawingID { get; set; }
        public long BomRecordID { get; set; }
        public long DrawDrawingID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BomRecord BomRecord { get; set; }
        public virtual DrawDrawing DrawDrawing { get; set; }
    }
}