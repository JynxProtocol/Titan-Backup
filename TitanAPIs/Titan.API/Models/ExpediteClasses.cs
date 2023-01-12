using System;

namespace Titan.API.Models
{
    public class ExpediteDataDTO
    {
        public string SupplierAccountName { get; set; }
        public string SupplierAccountNumber { get; set; }
        public string ContactName { get; set; }
        public string DocumentNo { get; set; }
        public decimal OnOrderQuantity { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string PO_Role { get; set; }
        public string ContactValue { get; set; }
        public string DocumentCreatedBy { get; set; }
        public long POPOrderReturnLineID { get; set; }
        public decimal RecievedQuantity { get; set; }
        public string SupplierPartRef { get; set; }
        public string ValidContactAddresses { get; set; }
    }

    // the format of a single row in the main report
    public class ReportDataLine
    {
        public string SupplierAccountName { get; set; }
        public string ContactName { get; set; }
        public string DocumentNo { get; set; }
        public decimal OnOrderQuantity { get; set; }
        public decimal RecievedQuantity { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string PO_Role { get; set; }
        public string ContactValue { get; set; }
        public string DocumentCreatedBy { get; set; }

        public ReportDataLine(ExpediteDataDTO expediteItem)
        {
            ContactName = expediteItem.ContactName;
            DocumentNo = expediteItem.DocumentNo;
            DeliveryDate = expediteItem.DeliveryDate;
            PO_Role = expediteItem.PO_Role;
            ContactValue = expediteItem.ContactValue;
            DocumentCreatedBy = expediteItem.DocumentCreatedBy;
            ItemCode = expediteItem.ItemCode;
            ItemDescription = expediteItem.ItemDescription;
            OnOrderQuantity = expediteItem.OnOrderQuantity;
            RecievedQuantity = expediteItem.RecievedQuantity;
            SupplierAccountName = expediteItem.SupplierAccountName;
        }
    }

    // the format of a single row in a supplier report
    public class SupplierDataLine
    {
        public string SupplierAccountNumber { get; set; }
        public string SupplierAccountName { get; set; }
        public string DocumentNo { get; set; }
        public decimal OnOrderQuantity { get; set; }
        public decimal RecievedQuantity { get; set; }
        public string ItemCode { get; set; }
        public string SupplierPartRef { get; set; }
        public string ItemDescription { get; set; }
        public DateTime DeliveryDate { get; set; }

        public SupplierDataLine(ExpediteDataDTO expediteItem)
        {
            SupplierAccountName = expediteItem.SupplierAccountName;
            DocumentNo = expediteItem.DocumentNo;
            OnOrderQuantity = expediteItem.OnOrderQuantity;
            RecievedQuantity = expediteItem.RecievedQuantity;
            ItemCode = expediteItem.ItemCode;
            SupplierPartRef = expediteItem.SupplierPartRef;
            ItemDescription = expediteItem.ItemDescription;
            SupplierAccountNumber = expediteItem.SupplierAccountNumber;
            DeliveryDate = expediteItem.DeliveryDate;
        }
    }
}
