// Write a program that retrieves the name and description of all categories in the Northwind DB.
namespace GetNameAndDescriptionOfAllCategories
{
    using System;
    using System.Data.SqlClient;

    public class Program
    {
        public static void Main(string[] args)
        {
            SqlConnection dbConnection = new SqlConnection("Server=.; " + "Database=Northwind; Integrated Security=true");

            dbConnection.Open();
            SqlCommand cmdCategories = new SqlCommand("SELECT CategoryID, CategoryName, Description FROM Categories", dbConnection);

            using (SqlDataReader reader = cmdCategories.ExecuteReader())
            {
                while (reader.Read())
                {
                    int categoryId = (int)reader["CategoryID"];
                    string categoryName = (string)reader["CategoryName"];
                    string description = (string)reader["Description"];
                    Console.WriteLine("{0}. {1} - {2}", categoryId, categoryName, description);
                }
            }

            dbConnection.Close();
        }
    }
}
