// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class CPCardProcessorTranType
    {
        public CPCardProcessorTranType()
        {
            CPTrans = new HashSet<CPTran>();
        }

        public long CPCardProcessorTranTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CPTran> CPTrans { get; set; }
    }
}