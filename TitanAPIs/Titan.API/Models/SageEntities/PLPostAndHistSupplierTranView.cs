﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class PLPostAndHistSupplierTranView
    {
        public long PLPostedSupplierTranID { get; set; }
        public long PLSupplierAccountID { get; set; }
        public long SYSTraderTranTypeID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionReference { get; set; }
        public long UniqueReferenceNumber { get; set; }
        public long? NominalAccountingPeriodID { get; set; }
        public decimal? GoodsValueInBaseCurrency { get; set; }
        public string Location { get; set; }
    }
}