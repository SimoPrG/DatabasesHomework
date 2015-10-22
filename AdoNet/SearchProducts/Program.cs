// Write a program that reads a string from the console and finds all products that contain this string.
// Ensure you handle correctly characters like ', %, ", \ and _.
namespace SearchProducts
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class Program
    {
        public static void Main()
        {
            SqlConnection dbConnection = new SqlConnection("Server=.; Database=Northwind; Integrated Security=true");

            Console.Write("Search for products: ");
            var searchString = Console.ReadLine();
            Console.WriteLine();

            dbConnection.Open();
            SqlCommand cmdCategories = new SqlCommand("SELECT ProductName FROM Products", dbConnection);

            var products = new List<string>();
            SqlDataReader reader = cmdCategories.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    string productName = (string)reader["ProductName"];
                    if (productName.Contains(searchString))
                    {
                        products.Add(productName);
                    }
                }
            }

            Console.WriteLine(string.Format(@"Products containing '{0}'", searchString));

            foreach (var product in products)
            {
                Console.WriteLine("- {0}", product);
            }

            dbConnection.Close();
        }
    }
}
