﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class POPOrderNotificationType
    {
        public POPOrderNotificationType()
        {
            POPOrderNotifications = new HashSet<POPOrderNotification>();
        }

        public long POPOrderNotificationTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<POPOrderNotification> POPOrderNotifications { get; set; }
    }
}