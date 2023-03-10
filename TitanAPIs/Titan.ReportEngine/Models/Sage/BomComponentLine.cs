// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class BomComponentLine
    {
        public BomComponentLine()
        {
            BomComponentReferences = new HashSet<BomComponentReference>();
            BomOperationComponents = new HashSet<BomOperationComponent>();
            WopBomComponentLineLinks = new HashSet<WopBomComponentLineLink>();
        }

        public long BomComponentLineID { get; set; }
        public long BomBuildPackageID { get; set; }
        public decimal SequenceNumber { get; set; }
        public long BomComponentLineTypeID { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public decimal ScrapPercentage { get; set; }
        public bool FixedQuantity { get; set; }
        public bool Suspended { get; set; }
        public bool ShowCommentOnReport { get; set; }
        public string DocumentFile { get; set; }
        public long? StockItemUnitID { get; set; }
        public string UnitOfMeasure { get; set; }
        public long? BomRecordID { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public bool ConfirmationRequired { get; set; }
        public bool? Consumed { get; set; }
        public bool UseSpecificSubassemblyVersion { get; set; }
        public string Instructions { get; set; }
        public string Notes { get; set; }
        public string BomCommentLine { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual BomBuildPackage BomBuildPackage { get; set; }
        public virtual BomComponentLineType BomComponentLineType { get; set; }
        public virtual ICollection<BomComponentReference> BomComponentReferences { get; set; }
        public virtual ICollection<BomOperationComponent> BomOperationComponents { get; set; }
        public virtual ICollection<WopBomComponentLineLink> WopBomComponentLineLinks { get; set; }
    }
}