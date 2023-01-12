﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class FAAsset
    {
        public FAAsset()
        {
            FADepreciationTrans = new HashSet<FADepreciationTran>();
        }

        public long FAAssetID { get; set; }
        public string AssetNumber { get; set; }
        public string Description { get; set; }
        public string BSDepreciationAccountNumber { get; set; }
        public string BSDepreciationAccountCostCentre { get; set; }
        public string BSDepreciationAccountDepartment { get; set; }
        public string PLDepreciationAccountNumber { get; set; }
        public string PLDepreciationAccountCostCentre { get; set; }
        public string PLDepreciationAccountDepartment { get; set; }
        public DateTime? AcquiredDate { get; set; }
        public decimal InitialValue { get; set; }
        public decimal ResidualBalance { get; set; }
        public decimal NetBookBalance { get; set; }
        public long? FADepreciationMethodID { get; set; }
        public decimal DepreciationPercent { get; set; }
        public long ExpectedLife { get; set; }
        public DateTime? LastDepreciationDate { get; set; }
        public string Manager { get; set; }
        public string Location { get; set; }
        public string Analysis1 { get; set; }
        public string Analysis2 { get; set; }
        public string Analysis3 { get; set; }
        public DateTime? DisposedDate { get; set; }
        public bool Active { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual FADepreciationMethod FADepreciationMethod { get; set; }
        public virtual ICollection<FADepreciationTran> FADepreciationTrans { get; set; }
    }
}