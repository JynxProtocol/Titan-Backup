﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TransactionGroup
    {
        public TransactionGroup()
        {
            TransactionTypes = new HashSet<TransactionType>();
        }

        public long TransactionGroupID { get; set; }
        public string TransactionGroupName { get; set; }

        public virtual ICollection<TransactionType> TransactionTypes { get; set; }
    }
}