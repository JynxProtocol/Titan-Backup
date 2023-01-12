﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class NLJournalTemplate
    {
        public NLJournalTemplate()
        {
            NLHeldJournals = new HashSet<NLHeldJournal>();
            NLJournalTemplateTrans = new HashSet<NLJournalTemplateTran>();
        }

        public long NLJournalTemplateID { get; set; }
        public long NLJournalTemplateTypeID { get; set; }
        public string Name { get; set; }
        public string CreatedByUserName { get; set; }
        public bool UnlimitedUses { get; set; }
        public int RepeatCount { get; set; }
        public int NumberOfTimesUsed { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DateLastUsed { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLJournalTemplateType NLJournalTemplateType { get; set; }
        public virtual ICollection<NLHeldJournal> NLHeldJournals { get; set; }
        public virtual ICollection<NLJournalTemplateTran> NLJournalTemplateTrans { get; set; }
    }
}