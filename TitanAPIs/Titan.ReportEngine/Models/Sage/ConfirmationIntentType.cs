// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class ConfirmationIntentType
    {
        public ConfirmationIntentType()
        {
            POPOrderReturnLineArches = new HashSet<POPOrderReturnLineArch>();
            POPOrderReturnLines = new HashSet<POPOrderReturnLine>();
            POPSettingDefaultFreeTextConfirmIntentNavigations = new HashSet<POPSetting>();
            POPSettingDefaultServLabConfirmIntentNavigations = new HashSet<POPSetting>();
            SOPOrderReturnLineArches = new HashSet<SOPOrderReturnLineArch>();
            SOPOrderReturnLines = new HashSet<SOPOrderReturnLine>();
            SOPSettingDefaultFreeTextConfirmIntentNavigations = new HashSet<SOPSetting>();
            SOPSettingDefaultServLabConfirmIntentNavigations = new HashSet<SOPSetting>();
        }

        public long ConfirmationIntentTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POPOrderReturnLineArch> POPOrderReturnLineArches { get; set; }
        public virtual ICollection<POPOrderReturnLine> POPOrderReturnLines { get; set; }
        public virtual ICollection<POPSetting> POPSettingDefaultFreeTextConfirmIntentNavigations { get; set; }
        public virtual ICollection<POPSetting> POPSettingDefaultServLabConfirmIntentNavigations { get; set; }
        public virtual ICollection<SOPOrderReturnLineArch> SOPOrderReturnLineArches { get; set; }
        public virtual ICollection<SOPOrderReturnLine> SOPOrderReturnLines { get; set; }
        public virtual ICollection<SOPSetting> SOPSettingDefaultFreeTextConfirmIntentNavigations { get; set; }
        public virtual ICollection<SOPSetting> SOPSettingDefaultServLabConfirmIntentNavigations { get; set; }
    }
}