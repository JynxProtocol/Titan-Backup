// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BLClauseAttribute
    {
        public long BLClauseAttributeID { get; set; }
        public long BLClauseID { get; set; }
        public string Name { get; set; }
        public string DataTypeName { get; set; }
        public string Value { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BLClause BLClause { get; set; }
    }
}