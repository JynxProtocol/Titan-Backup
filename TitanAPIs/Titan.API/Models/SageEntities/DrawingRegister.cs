﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class DrawingRegister
    {
        public DrawingRegister()
        {
            DrawingAttachedDocuments = new HashSet<DrawingAttachedDocument>();
            DrawingRevisions = new HashSet<DrawingRevision>();
        }

        public long ID { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string RevisionNumber { get; set; }
        public string Revision { get; set; }
        public string Location { get; set; }
        public string DesignPackage { get; set; }
        public DateTime? Date { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string DiskNumber { get; set; }
        public string DiskType { get; set; }
        public string StockCode { get; set; }
        public string StockDescription { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public virtual ICollection<DrawingAttachedDocument> DrawingAttachedDocuments { get; set; }
        public virtual ICollection<DrawingRevision> DrawingRevisions { get; set; }
    }
}