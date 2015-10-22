namespace EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        static void Main()
        {
            // 1. Create a DAO class with static methods which provide functionality for
            // inserting, modifying and deleting customers.
            CustomersRepository.AddCustomerRecord("PESHO", "Telerik");

            CustomersRepository.UpdateCustomerRecord("PESHO", "Pesho");

            CustomersRepository.DeleteCustomerRecored("PESHO");

            //2. Write a method that finds all customers who have orders made in 1997 and shipped to Canada.
            Console.WriteLine("All customers who have orders made in 1997 and shipped to Canada:");
            var customers = CustomersRepository.GetCustomersWithOrdersIn1997ToCanada();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.CompanyName);
            }

            // 5. Write a method that finds all the sales by specified region and period (start / end dates).
            Console.WriteLine("SaleIDs in October 1996 for NM region:");
            var sales = FindAllSales("NM", new DateTime(1996, 10, 1), new DateTime(1996, 10, 31));
            foreach (var sale in sales)
            {
                Console.WriteLine(sale.OrderID);
            }

            //6.
            //Create a database called NorthwindTwin with the same structure as Northwind using the features from DbContext.
            //Find for the API for schema generation in MSDN or in Google.
            /* 
            * Steps to reproduse the DB:
            * 1. Open NorthwindEntities.edmx
            * 2. Right button click on the empty space between the tables
            * 3. Click Generate Database from model - it will generate a SQL script
            * 4. Edit the USE [Northwind]; to the name of the database you want to clone the current DB to [NorthwindTwin].
            * 5. Run the script;
            */

            // 7.
            // Try to open two different data contexts and perform concurrent changes on the same records.
            // What will happen at SaveChanges()?
            // How to deal with it?
            DifferentContexts();
        }

        // 5. Write a method that finds all the sales by specified region and period (start / end dates).
        public static IEnumerable<Order> FindAllSales(string region, DateTime startDate, DateTime endDate)
        {
            using (var northwindEntities = new NorthwindEntities())
            {
                //return (from order in northwindEntities.Orders
                //        where order.ShipRegion == region && startDate <= order.ShippedDate && order.ShippedDate <= endDate
                //        select order).ToList();

                return
                    northwindEntities.Orders.Where(
                        o => o.ShipRegion == region && startDate <= o.ShippedDate && o.ShippedDate <= endDate).ToList();
            }
        }

        // 7.
        // Try to open two different data contexts and perform concurrent changes on the same records.
        // What will happen at SaveChanges()?
        // How to deal with it?
        public static void DifferentContexts()
        {
            using (NorthwindEntities db1 = new NorthwindEntities(), db2 = new NorthwindEntities())
            {
                var customer1 = db1.Customers.FirstOrDefault();

                Console.WriteLine(customer1.ContactName);

                customer1.ContactName = "Ivan";

                var customer2 = db2.Customers.FirstOrDefault();

                Console.WriteLine(customer2.ContactName);

                customer2.ContactName = "Pesho";

                db1.SaveChanges();
                db2.SaveChanges();
            }
        }

        // 8.
        // By inheriting the Employee entity class create a class which allows employees to access their corresponding
        // territories as property of type EntitySet<T>.
        public static void ShowEmployeeTerritories()
        {
            using (var db = new NorthwindEntities())
            {
                var employee = db.Employees.FirstOrDefault();
                foreach (var correspondingTerritory in employee.CorrespondingTerritories)
                {
                    Console.WriteLine(correspondingTerritory.TerritoryDescription);
                }
            }
        }
    }
}
