// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class FADepreciationMethod
    {
        public FADepreciationMethod()
        {
            FAAssets = new HashSet<FAAsset>();
        }

        public long FADepreciationMethodID { get; set; }
        public string DepreciationMethodName { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<FAAsset> FAAssets { get; set; }
    }
}