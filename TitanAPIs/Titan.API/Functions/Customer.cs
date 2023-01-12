using System.Linq;

namespace Titan.API.Functions
{
    public static class Customer
    {
        public static Titan.API.Models.Customer CopyCustomerToTitanIfNotThere(int CusID,
            TitanContext Titan, SAGEContext Sage)
        {
            if (!Titan.Customers.Any(cus => cus.CusID == CusID))
            {
                var SageCustomer = Sage.SLCustomerAccounts
                    .Where(cus => cus.SLCustomerAccountID == CusID)
                    .Single();

                Titan.Customers.Add(new Models.Customer
                {
                    CusID = SageCustomer.SLCustomerAccountID,
                    CustomerAccountNumber = SageCustomer.CustomerAccountNumber,
                    CustomerName = SageCustomer.CustomerAccountName
                });

                Titan.SaveChanges();
            }

            return Titan.Customers
                .Where(cus => cus.CusID == CusID).SingleOrDefault();
        }
    }
}
