﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLJournalType
    {
        public NLJournalType()
        {
            NLHeldJournals = new HashSet<NLHeldJournal>();
        }

        public long NLJournalTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NLHeldJournal> NLHeldJournals { get; set; }
    }
}