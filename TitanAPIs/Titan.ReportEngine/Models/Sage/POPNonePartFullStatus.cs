// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class POPNonePartFullStatus
    {
        public POPNonePartFullStatus()
        {
            POPRequisitionPOPRequisitionAuthorisationStatuses = new HashSet<POPRequisition>();
            POPRequisitionPOPRequisitionRejectedStatuses = new HashSet<POPRequisition>();
        }

        public long POPNonePartFullStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POPRequisition> POPRequisitionPOPRequisitionAuthorisationStatuses { get; set; }
        public virtual ICollection<POPRequisition> POPRequisitionPOPRequisitionRejectedStatuses { get; set; }
    }
}