﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TSPerson
    {
        public TSPerson()
        {
            PCProjectUserProfiles = new HashSet<PCProjectUserProfile>();
            TSAuthGroupAuthorisers = new HashSet<TSAuthGroupAuthoriser>();
            TSAuthGroupMembers = new HashSet<TSAuthGroupMember>();
            TSClaimSheets = new HashSet<TSClaimSheet>();
            TSHumanResources = new HashSet<TSHumanResource>();
            TSResourceHierarchyPeople = new HashSet<TSResourceHierarchyPerson>();
            TSTimeRecords = new HashSet<TSTimeRecord>();
            TSUserProfiles = new HashSet<TSUserProfile>();
        }

        public long TSPersonID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string NINumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }
        public string FileAsName { get; set; }
        public string CompanyName { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual ICollection<PCProjectUserProfile> PCProjectUserProfiles { get; set; }
        public virtual ICollection<TSAuthGroupAuthoriser> TSAuthGroupAuthorisers { get; set; }
        public virtual ICollection<TSAuthGroupMember> TSAuthGroupMembers { get; set; }
        public virtual ICollection<TSClaimSheet> TSClaimSheets { get; set; }
        public virtual ICollection<TSHumanResource> TSHumanResources { get; set; }
        public virtual ICollection<TSResourceHierarchyPerson> TSResourceHierarchyPeople { get; set; }
        public virtual ICollection<TSTimeRecord> TSTimeRecords { get; set; }
        public virtual ICollection<TSUserProfile> TSUserProfiles { get; set; }
    }
}