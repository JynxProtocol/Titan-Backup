// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSProviderTypeAttribute
    {
        public SYSProviderTypeAttribute()
        {
            SYSProviderConfigurationAttributes = new HashSet<SYSProviderConfigurationAttribute>();
        }

        public long SYSProviderTypeAttributeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long SYSProviderTypeID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool IsSensitive { get; set; }
        public long SYSProviderTypeAttributeTypeID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSProviderType SYSProviderType { get; set; }
        public virtual ICollection<SYSProviderConfigurationAttribute> SYSProviderConfigurationAttributes { get; set; }
    }
}