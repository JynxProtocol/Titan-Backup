﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSNotificationPriorityType
    {
        public SYSNotificationPriorityType()
        {
            SYSNotifications = new HashSet<SYSNotification>();
        }

        public long SYSNotificationPriorityTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SYSNotification> SYSNotifications { get; set; }
    }
}