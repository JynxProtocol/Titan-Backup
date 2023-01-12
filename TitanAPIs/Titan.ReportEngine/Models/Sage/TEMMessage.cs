﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class TEMMessage
    {
        public TEMMessage()
        {
            TEMMessageEvents = new HashSet<TEMMessageEvent>();
            TEMMessageOrders = new HashSet<TEMMessageOrder>();
        }

        public long TEMMessageID { get; set; }
        public long? TEMMessageTypeID { get; set; }
        public string Sender { get; set; }
        public string ReplyTo { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public string AccRef { get; set; }
        public short LinesUnmatched { get; set; }
        public long? TEMStatusID { get; set; }
        public DateTime? MessageDate { get; set; }
        public DateTime MessageTime { get; set; }
        public long? TEMLocationID { get; set; }
        public string ReceivedXMLDoc { get; set; }
        public string AmendedXMLDoc { get; set; }
        public string MAPIGUID { get; set; }
        public long RemoteMessageIdentifier { get; set; }
        public bool ValidCheckSum { get; set; }
        public bool Unread { get; set; }
        public string CompanyName { get; set; }
        public string FromResolved { get; set; }
        public long? POPInvoiceID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual POPInvCredDispute POPInvoice { get; set; }
        public virtual TEMLocation TEMLocation { get; set; }
        public virtual TEMMessageType TEMMessageType { get; set; }
        public virtual TEMStatus TEMStatus { get; set; }
        public virtual ICollection<TEMMessageEvent> TEMMessageEvents { get; set; }
        public virtual ICollection<TEMMessageOrder> TEMMessageOrders { get; set; }
    }
}