using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Titan.Data;

namespace Titan.Functions
{
    public class Lookups
    {
        public static List<SelectListItem> GetList(int lookuptype)
        {
            var mydata = new TitanEntities();
            List<SelectListItem> myList = new();

            myList = (from lup in mydata.LookUpTicketDatas
                      where lup.lupType == lookuptype
                      select new SelectListItem { Value = lup.lupText, Text = lup.lupText }).OrderBy(SelectListItem => SelectListItem.Text).ToList();
            return myList;
        }

        public static List<SelectListItem> GetPriority()
        {
            var myList = new List<SelectListItem>();
            var myitem = new SelectListItem
            {
                Value = "0",
                Text = "Low"
            };

            myList.Add(myitem);

            myitem = new SelectListItem
            {
                Value = "1",
                Text = "Medium"
            };

            myList.Add(myitem);

            myitem = new SelectListItem
            {
                Value = "2",
                Text = "High"
            };

            myList.Add(myitem);

            return (myList);
        }

        public static List<SelectListItem> GetFrequency()
        {
            var myList = new List<SelectListItem>();
            var myitem = new SelectListItem
            {
                Value = "0",
                Text = "Once"
            };

            myList.Add(myitem);

            myitem = new SelectListItem
            {
                Value = "1",
                Text = "Hardly Ever"
            };

            myList.Add(myitem);

            myitem = new SelectListItem
            {
                Value = "2",
                Text = "Occasionaly"
            };

            myList.Add(myitem);

            myitem = new SelectListItem
            {
                Value = "3",
                Text = "Every Time"
            };

            myList.Add(myitem);

            return (myList);
        }

        public static List<SelectListItem> GetDeptList()
        {
            var mydata = new AuthEntities();
            List<SelectListItem> myList = new();

            myList = (from dept in mydata.Departments
                      select new SelectListItem { Value = dept.UsrDept, Text = dept.UsrDept }).OrderBy(SelectListItem => SelectListItem.Text).ToList();

            return myList;
        }

        public static List<SelectListItem> GetEngType()
        {
            var mydata = new TitanEntities();
            List<SelectListItem> myList = new();

            myList = (from typ in mydata.LookUpEngTypes
                      select new SelectListItem { Value = typ.LookupText, Text = typ.LookupText }).OrderBy(SelectListItem => SelectListItem.Text).ToList();

            return myList;
        }

        public static List<SelectListItem> GetAWKSalesType()
        {
            var mydata = new TitanEntities();
            List<SelectListItem> myList = new();

            myList = (from typ in mydata.LookUpAWKSalesTypes
                      select new SelectListItem { Value = typ.LookupText, Text = typ.LookupText }).OrderBy(SelectListItem => SelectListItem.Text).ToList();

            return myList;
        }

        public static List<SelectListItem> GetCustomerList()
        {
            var mysagedata = new SageEntities();

            List<SelectListItem> myList = mysagedata.SLCustomerAccounts.Select(
                        cus => new SelectListItem
                        { Value = cus.SLCustomerAccountID.ToString(), Text = cus.CustomerAccountName }
                        ).ToList();

            return myList;
        }

        public static List<SelectListItem> GetColourList()
        {
            var mydata = new TitanEntities();
            List<SelectListItem> myList = new();

            myList = (from col in mydata.LookUpColours
                      select new SelectListItem { Value = col.LookupText, Text = col.LookupText }).OrderBy(SelectListItem => SelectListItem.Text).ToList();

            return myList;
        }

        public static List<SelectListItem> GetWarrantylist()
        {
            var mydata = new TitanEntities();
            List<SelectListItem> myList = new();

            myList = (from col in mydata.LookUpWarranties
                      select new SelectListItem { Value = col.LookupText, Text = col.LookupText }).OrderBy(SelectListItem => SelectListItem.Text).ToList();

            return myList;
        }


        public static List<SelectListItem> GetLeadTimelist()
        {
            var mydata = new TitanEntities();
            List<SelectListItem> myList = new();

            myList = (from col in mydata.LookUpLeadTimes
                      select new SelectListItem { Value = col.LookupText, Text = col.LookupText }).OrderBy(SelectListItem => SelectListItem.Text).ToList();

            return myList;
        }

        public static List<SelectListItem> GetWorkRequiredlist()
        {
            var mydata = new TitanEntities();
            List<SelectListItem> myList = new();

            myList = (from col in mydata.LookUpWorkRequireds
                      select new SelectListItem { Value = col.LookupText, Text = col.LookupText }).OrderBy(SelectListItem => SelectListItem.Text).ToList();

            return myList;
        }

        public static List<SelectListItem> GetDespatchMethodlist()
        {
            var mydata = new TitanEntities();
            List<SelectListItem> myList = new();

            myList = (from col in mydata.LookUpDespatchMethods
                      select new SelectListItem { Value = col.LookupText, Text = col.LookupText }).OrderBy(SelectListItem => SelectListItem.Text).ToList();

            return myList;
        }

        public static List<SelectListItem> GetDeliveryTermslist()
        {
            var mydata = new TitanEntities();
            List<SelectListItem> myList = new();

            myList = (from col in mydata.LookUpDeliveryTerms
                      select new SelectListItem { Value = col.LookupText, Text = col.LookupText }).OrderBy(SelectListItem => SelectListItem.Text).ToList();

            return myList;
        }
    }
}