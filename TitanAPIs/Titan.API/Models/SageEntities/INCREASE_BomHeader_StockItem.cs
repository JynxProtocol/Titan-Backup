﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class INCREASE_BomHeader_StockItem
    {
        public string Stock_item_code { get; set; }
        public string Stock_item_name { get; set; }
        public string Product_group { get; set; }
        public short? Tax_code { get; set; }
        public string Stock_item_description { get; set; }
        public string Manufacturer_s_name { get; set; }
        public string Manufacturer_s_part_number { get; set; }
        public string Commodity_code { get; set; }
        public short? Net_mass { get; set; }
        public short? Stock_take_days { get; set; }
        public short? Asset_of_stock___account_number { get; set; }
        public string Asset_of_stock___cost_centre { get; set; }
        public string Asset_of_stock___department { get; set; }
        public short? Revenue___account_number { get; set; }
        public string Revenue___cost_centre { get; set; }
        public string Revenue___department { get; set; }
        public short? Accrued_receipts___account_number { get; set; }
        public string Accrued_receipts___cost_centre { get; set; }
        public string Accrued_receipts___department { get; set; }
        public short? Issues___account_number { get; set; }
        public string Issues___cost_centre { get; set; }
        public string Issues___department { get; set; }
        public string Supplier { get; set; }
        public short? Supplier_lead_time { get; set; }
        public string Supplier_lead_time_unit { get; set; }
        public short? Supplier_minimum_quantity { get; set; }
        public short? Supplier_usual_order_quantity { get; set; }
        public string Supplier_part_number { get; set; }
        public string Alternative_item { get; set; }
        public string Alternative_item_name { get; set; }
    }
}