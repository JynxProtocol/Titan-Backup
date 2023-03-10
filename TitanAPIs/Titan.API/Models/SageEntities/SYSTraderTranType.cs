// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class SYSTraderTranType
    {
        public SYSTraderTranType()
        {
            PLHistoricalSupplierTrans = new HashSet<PLHistoricalSupplierTran>();
            PLPendSupplierBatches = new HashSet<PLPendSupplierBatch>();
            PLPendSupplierTrans = new HashSet<PLPendSupplierTran>();
            PLPostedSupplierTrans = new HashSet<PLPostedSupplierTran>();
            SLHistoricalCustomerTrans = new HashSet<SLHistoricalCustomerTran>();
            SLPendCustomerBatches = new HashSet<SLPendCustomerBatch>();
            SLPostedCustomerTrans = new HashSet<SLPostedCustomerTran>();
            SYSCorrections = new HashSet<SYSCorrection>();
        }

        public long SYSTraderTranTypeID { get; set; }
        public string Name { get; set; }
        public string SalesName { get; set; }
        public string PurchaseName { get; set; }
        public string SalesShortName { get; set; }
        public string PurchaseShortName { get; set; }

        public virtual ICollection<PLHistoricalSupplierTran> PLHistoricalSupplierTrans { get; set; }
        public virtual ICollection<PLPendSupplierBatch> PLPendSupplierBatches { get; set; }
        public virtual ICollection<PLPendSupplierTran> PLPendSupplierTrans { get; set; }
        public virtual ICollection<PLPostedSupplierTran> PLPostedSupplierTrans { get; set; }
        public virtual ICollection<SLHistoricalCustomerTran> SLHistoricalCustomerTrans { get; set; }
        public virtual ICollection<SLPendCustomerBatch> SLPendCustomerBatches { get; set; }
        public virtual ICollection<SLPostedCustomerTran> SLPostedCustomerTrans { get; set; }
        public virtual ICollection<SYSCorrection> SYSCorrections { get; set; }
    }
}