﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CBPaymentFrequency
    {
        public CBPaymentFrequency()
        {
            CBDirectDebitTrans = new HashSet<CBDirectDebitTran>();
        }

        public long CBPaymentFrequencyID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CBDirectDebitTran> CBDirectDebitTrans { get; set; }
    }
}