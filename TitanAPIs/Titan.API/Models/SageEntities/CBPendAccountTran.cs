﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class CBPendAccountTran
    {
        public long CBPendAccountTranID { get; set; }
        public long CBAccountID { get; set; }
        public long CBTranTypeID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionReference { get; set; }
        public string SecondReference { get; set; }
        public decimal ChequeValue { get; set; }
        public decimal ChequeDiscountValue { get; set; }
        public long ChequeSYSCurrencyID { get; set; }
        public decimal ChequeToBaseCurrExchangeRate { get; set; }
        public decimal ChequeToAccountExchangeRate { get; set; }
        public DateTime PostedDate { get; set; }
        public long Source { get; set; }
        public long UniqueReferenceNumber { get; set; }
        public int UserNumber { get; set; }
        public string UserName { get; set; }
        public string NominalOrTraderAccountNumber { get; set; }
        public string NominalAccountCostCentre { get; set; }
        public string NominalAccountDepartment { get; set; }
        public string ChequeDescription { get; set; }
        public decimal StatementValue { get; set; }
        public int? StatementPageNumber { get; set; }
        public DateTime? LastStatementDate { get; set; }
        public long? CBDirectDebitTranID { get; set; }
        public byte[] OpLock { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public long? NominalAccountingPeriodID { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual CBAccount CBAccount { get; set; }
        public virtual CBDirectDebitTran CBDirectDebitTran { get; set; }
        public virtual CBTranType CBTranType { get; set; }
        public virtual SYSAccountingPeriod NominalAccountingPeriod { get; set; }
    }
}