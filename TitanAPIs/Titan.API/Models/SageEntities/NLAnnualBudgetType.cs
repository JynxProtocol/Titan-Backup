// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLAnnualBudgetType
    {
        public NLAnnualBudgetType()
        {
            NLAccountYearValues = new HashSet<NLAccountYearValue>();
        }

        public long NLAnnualBudgetTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NLAccountYearValue> NLAccountYearValues { get; set; }
    }
}