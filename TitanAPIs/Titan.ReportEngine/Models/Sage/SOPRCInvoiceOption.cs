// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SOPRCInvoiceOption
    {
        public SOPRCInvoiceOption()
        {
            SOPSettings = new HashSet<SOPSetting>();
        }

        public long SOPRCInvoiceOptionID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SOPSetting> SOPSettings { get; set; }
    }
}