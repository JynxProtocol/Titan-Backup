// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.API.Models
{
    public partial class AWKSetting
    {
        public int ID { get; set; }
        public string AWKStorage { get; set; }
        public bool DatabaseStorage { get; set; }
        public bool UpdatePriceBookFromDetail { get; set; }
        public int PixelSize { get; set; }
        public bool EmailOnApproval { get; set; }
        public string EmailAddresses { get; set; }
    }
}