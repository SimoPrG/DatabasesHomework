// 1. Create a DAO class with static methods which provide functionality for inserting, modifying and deleting customers.
namespace EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CustomersRepository
    {
        public static void DeleteCustomerRecored(string customerId)
        {
            using (var northwindEntities = new NorthwindEntities())
            {
                var customer = CustomersRepository.GetCustomerById(northwindEntities, customerId);
                northwindEntities.Customers.Remove(customer);
                northwindEntities.SaveChanges();
            }
        }

        public static void UpdateCustomerRecord(string customerId, string contactName)
        {
            using (var northwindEntities = new NorthwindEntities())
            {
                var customer = CustomersRepository.GetCustomerById(northwindEntities, customerId);
                customer.ContactName = contactName;
                northwindEntities.SaveChanges();
            }
        }

        public static void AddCustomerRecord(string customerId, string companyName)
        {
            using (var northwindEntities = new NorthwindEntities())
            {
                northwindEntities.Customers.Add(new Customer
                {
                    CustomerID = customerId,
                    CompanyName = companyName
                });

                northwindEntities.SaveChanges();
            }
        }

        //2. Write a method that finds all customers who have orders made in 1997 and shipped to Canada.
        // To the examiner: Check the commented code below! I solved this problem in 3 different ways.
        public static IEnumerable<Customer> GetCustomersWithOrdersIn1997ToCanada()
        {
            using (var northwindEntities = new NorthwindEntities())
            {
                //Console.WriteLine(from c in northwindEntities.Customers
                //                  join o in northwindEntities.Orders on c.CustomerID equals o.CustomerID
                //                  where o.ShipCountry == "Canada" && o.OrderDate.Value.Year == 1997
                //                  orderby c.CompanyName
                //                  select c);

                //return (from c in northwindEntities.Customers
                //       join o in northwindEntities.Orders on c.CustomerID equals o.CustomerID
                //       where o.ShipCountry == "Canada" && o.OrderDate.Value.Year == 1997
                //       orderby c.CompanyName
                //       select c).ToList();

                //Console.WriteLine(
                //                  northwindEntities.Orders
                //                  .Where(o => o.ShipCountry == "Canada" && o.OrderDate.Value.Year == 1997)
                //                  .Join(
                //                  northwindEntities.Customers,
                //                  o => o.CustomerID,
                //                  c => c.CustomerID,
                //                  (o, c) => c)
                //                  .OrderBy(c => c.CompanyName));

                //return
                //    northwindEntities.Orders
                //    .Where(o => o.ShipCountry == "Canada" && o.OrderDate.Value.Year == 1997)
                //    .Join(
                //    northwindEntities.Customers,
                //    o => o.CustomerID,
                //    c => c.CustomerID,
                //    (o, c) => c)
                //    .OrderBy(c => c.CompanyName)
                //    .ToList();

                //Console.WriteLine(
                //    northwindEntities.Customers
                //    .Select(c => c)
                //    .Where(c => c.Orders.Any(o => o.ShipCountry == "Canada" && o.OrderDate.Value.Year == 1997)));

                //return
                //    northwindEntities.Customers
                //    .Select(c => c)
                //    .Where(c => c.Orders.Any(o => o.ShipCountry == "Canada" && o.OrderDate.Value.Year == 1997))
                //    .ToList();

                // 4. Implement previous by using native SQL query and executing it through the DbContext.
                string query =
@"SELECT c.CustomerID,
       c.CompanyName,
	   c.ContactName,
	   c.ContactTitle,
	   c.Address,
	   c.City,
	   c.Region,
	   c.PostalCode,
	   c.Country,
	   c.Phone,
	   c.Fax
FROM Customers c
WHERE EXISTS (SELECT * FROM Orders o
			  WHERE c.CustomerID = o.CustomerID AND o.ShipCountry = 'Canada' AND YEAR(o.OrderDate) = 1997)";

                return northwindEntities.Database.SqlQuery<Customer>(query).ToList();
            }
        }

        private static Customer GetCustomerById(NorthwindEntities northwindEntities, string customerId)
        {
            return northwindEntities.Customers.FirstOrDefault(c => c.CustomerID == customerId);
        }
    }
}
