﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class NLJournalTranTaxType
    {
        public NLJournalTranTaxType()
        {
            NLHeldJournalTrans = new HashSet<NLHeldJournalTran>();
            NLJournalTemplateTrans = new HashSet<NLJournalTemplateTran>();
        }

        public long NLJournalTranTaxTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NLHeldJournalTran> NLHeldJournalTrans { get; set; }
        public virtual ICollection<NLJournalTemplateTran> NLJournalTemplateTrans { get; set; }
    }
}