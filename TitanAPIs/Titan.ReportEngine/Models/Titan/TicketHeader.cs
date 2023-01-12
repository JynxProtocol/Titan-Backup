﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Titan.ReportEngine.Models.Titan
{
    public partial class TicketHeader
    {
        public int TicketId { get; set; }
        public string TicketTitle { get; set; }
        public string TicketType { get; set; }
        public int? TicketStatus { get; set; }
        public string TicketCreatedBy { get; set; }
        public string TicketAssignedTo { get; set; }
        public string TicketDetails { get; set; }
        public int? TicketPriority { get; set; }
        public string TicketContactCompany { get; set; }
        public string TicketContact { get; set; }
        public string TicketContactPhoneNumber { get; set; }
        public string TicketContactEmail { get; set; }
        public DateTime? TicketTargetDate { get; set; }
        public DateTime? TicketCreatedDate { get; set; }
        public DateTime? TicketLastedUpdated { get; set; }
        public string TicketAssignedDept { get; set; }
        public DateTime? TicketClosedDate { get; set; }
        public string TicketRasiedDept { get; set; }
        public TimeSpan? TicketTime { get; set; }
        public string TicketURL { get; set; }
        public string TicketActionPerformed { get; set; }
        public string TicketErrorMessage { get; set; }
        public string TicketExpectedResult { get; set; }
        public string TicketActualResult { get; set; }
        public int? TicketFrequency { get; set; }
        public string FeatureSuggestion { get; set; }
        public string FeatureProblem { get; set; }
        public string FeatureWorkround { get; set; }
        public string FeatureComments { get; set; }
        public string EngineeringType { get; set; }
    }
}