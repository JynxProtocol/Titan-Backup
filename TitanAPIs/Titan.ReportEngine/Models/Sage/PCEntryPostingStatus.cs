// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class PCEntryPostingStatus
    {
        public PCEntryPostingStatus()
        {
            PCProjectEntryPostings = new HashSet<PCProjectEntryPosting>();
        }

        public long PCEntryPostingStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PCProjectEntryPosting> PCProjectEntryPostings { get; set; }
    }
}