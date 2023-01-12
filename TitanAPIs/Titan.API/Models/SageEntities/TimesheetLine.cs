﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class TimesheetLine
    {
        public long ID { get; set; }
        public long HeaderID { get; set; }
        public string JobNumber { get; set; }
        public string OpReference { get; set; }
        public string OpDescription { get; set; }
        public DateTime Date { get; set; }
        public string Started { get; set; }
        public string Finished { get; set; }
        public decimal Completed { get; set; }
        public decimal LabMins { get; set; }
        public decimal MachMins { get; set; }
        public decimal SetupMins { get; set; }
        public decimal LabCost { get; set; }
        public decimal MachCost { get; set; }
        public decimal SetupCost { get; set; }
        public string LabDebit { get; set; }
        public string LabCredit { get; set; }
        public string MachDebit { get; set; }
        public string MachCredit { get; set; }
        public string SetupDebit { get; set; }
        public string SetupCredit { get; set; }
        public decimal LabRate { get; set; }
        public decimal MachRate { get; set; }
        public decimal SetupRate { get; set; }
        public bool PieceWork { get; set; }
        public decimal PieceRate { get; set; }
        public decimal PieceQty { get; set; }
        public bool SubContract { get; set; }
        public long OpRecNo { get; set; }
        public string Notes { get; set; }
        public string OpOnStage { get; set; }
        public bool NonPrinting { get; set; }
        public string StageRef { get; set; }
        public string CategoryRef { get; set; }
        public string CategoryDesc { get; set; }
        public string NonChargeableRef { get; set; }
        public string JobDebit { get; set; }
        public long WoID { get; set; }
        public long StageID { get; set; }

        public virtual TimesheetHeader Header { get; set; }
    }
}