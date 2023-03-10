// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.ReportEngine.Models.Sage
{
    public partial class SYSTaxPeriodRateBalance
    {
        public SYSTaxPeriodRateBalance()
        {
            SYSTaxTrans = new HashSet<SYSTaxTran>();
        }

        public long SYSTaxPeriodRateBalanceID { get; set; }
        public long SYSTaxPeriodID { get; set; }
        public long SYSTaxRateID { get; set; }
        public DateTime? DateTaxReturnLastPrinted { get; set; }
        public DateTime? DateTaxTransactionsLastDeleted { get; set; }
        public decimal InputGoodsValue { get; set; }
        public decimal InputTaxValue { get; set; }
        public decimal OutputGoodsValue { get; set; }
        public decimal OutputTaxValue { get; set; }
        public DateTime? InputLastTransactionDate { get; set; }
        public DateTime? OutputLastTransactionDate { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual SYSTaxPeriod SYSTaxPeriod { get; set; }
        public virtual SYSTaxRate SYSTaxRate { get; set; }
        public virtual ICollection<SYSTaxTran> SYSTaxTrans { get; set; }
    }
}