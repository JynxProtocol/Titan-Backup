﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class MsmUserSetting
    {
        public long MsmUserSettingID { get; set; }
        public string UserName { get; set; }
        public bool ShowWelcomePageAtStartup { get; set; }
        public bool? ShowImageThumbnails { get; set; }
        /// <summary>
        /// Required by ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// GetDate()
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}