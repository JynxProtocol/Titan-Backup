﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class RTLErrorCode
    {
        public long ErrorId { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public bool IsWarning { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}