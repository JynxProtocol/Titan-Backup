// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLReconciliationEnquirySetting
    {
        public long NLReconciliationEnquirySettingID { get; set; }
        public long NLReconciliationEnquiryTypeID { get; set; }
        public long NLNominalAccountID { get; set; }

        public virtual NLNominalAccount NLNominalAccount { get; set; }
        public virtual NLReconciliationEnquiryType NLReconciliationEnquiryType { get; set; }
    }
}