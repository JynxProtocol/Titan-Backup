// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TSCategoryComponent
    {
        public TSCategoryComponent()
        {
            InverseParentCategoryComponent = new HashSet<TSCategoryComponent>();
            TSClaimRecordTSCategories = new HashSet<TSClaimRecord>();
            TSClaimRecordTSCategoryGroups = new HashSet<TSClaimRecord>();
        }

        public long TSCategoryComponentID { get; set; }
        public long? ParentCategoryComponentID { get; set; }
        public long TSCategoryComponentTypeID { get; set; }
        public long TSCategoryCompStatusTypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string NominalAccountNumber { get; set; }
        public string NominalCostCentre { get; set; }
        public string NominalDepartment { get; set; }
        public long? SYSTaxCodeID { get; set; }
        public decimal UnitRate { get; set; }
        public bool? UnitBased { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual TSCategoryComponent ParentCategoryComponent { get; set; }
        public virtual TSCategoryCompStatusType TSCategoryCompStatusType { get; set; }
        public virtual TSCategoryCompType TSCategoryComponentType { get; set; }
        public virtual ICollection<TSCategoryComponent> InverseParentCategoryComponent { get; set; }
        public virtual ICollection<TSClaimRecord> TSClaimRecordTSCategories { get; set; }
        public virtual ICollection<TSClaimRecord> TSClaimRecordTSCategoryGroups { get; set; }
    }
}