﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CBAccountType
    {
        public CBAccountType()
        {
            CBAccounts = new HashSet<CBAccount>();
        }

        public long CBAccountTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CBAccount> CBAccounts { get; set; }
    }
}