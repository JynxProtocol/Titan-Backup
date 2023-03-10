// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSCorrection
    {
        public long SYSCorrectionID { get; set; }
        public long OriginalURN { get; set; }
        public long ReversalURN { get; set; }
        public long NewURN { get; set; }
        public long Source { get; set; }
        public long? SYSTraderTranTypeID { get; set; }
        public long? NLNominalTranTypeID { get; set; }
        public DateTime OriginalTransactionDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public int UserNumber { get; set; }
        public string UserName { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual NLNominalTranType NLNominalTranType { get; set; }
        public virtual SYSTraderTranType SYSTraderTranType { get; set; }
    }
}