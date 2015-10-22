// Write a program that retrieves from the Northwind database all product categories and the names of the products in each category
namespace GetAllProductCategories
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class Program
    {
        public static void Main()
        {
            SqlConnection dbCon = new SqlConnection("Server=.; " + "Database=Northwind; Integrated Security=true");

            dbCon.Open();

            SqlCommand cmdCategories = new SqlCommand(
                "SELECT ProductName, CategoryName FROM Products AS p " +
                "JOIN Categories AS c " +
                "ON p.CategoryID = c.CategoryID", dbCon);

            var categories = new Dictionary<string, List<string>>();
            SqlDataReader reader = cmdCategories.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    string categoryName = (string)reader["CategoryName"];
                    string productName = (string)reader["ProductName"];

                    if (categories.ContainsKey(categoryName))
                    {
                        categories[categoryName].Add(productName);
                    }
                    else
                    {
                        categories.Add(categoryName, new List<string>());
                        categories[categoryName].Add(productName);
                    }
                }
            }

            foreach (var category in categories)
            {
                Console.WriteLine("{0} - {1}", category.Key, string.Join(", ", category.Value));
                Console.WriteLine();
            }

            dbCon.Close();
        }
    }
}
