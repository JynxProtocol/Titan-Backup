﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class OpOperationPhase
    {
        public OpOperationPhase()
        {
            BomCosts = new HashSet<BomCost>();
            BomOperationResources = new HashSet<BomOperationResource>();
            OpOperationResources = new HashSet<OpOperationResource>();
        }

        public long OpOperationPhaseID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<BomCost> BomCosts { get; set; }
        public virtual ICollection<BomOperationResource> BomOperationResources { get; set; }
        public virtual ICollection<OpOperationResource> OpOperationResources { get; set; }
    }
}