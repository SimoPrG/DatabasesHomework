// Write a program that retrieves from the Northwind sample database in MS SQL Server the number of rows in the Categories table.
namespace GetNumberOfCategoryRows
{
    using System;
    using System.Data.SqlClient;

    public class Program
    {
        public static void Main()
        {
            // Setup the connection
            SqlConnection dbConnection = new SqlConnection("Server=.; " + "Database=Northwind; Integrated Security=true");

            dbConnection.Open();
            SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM Categories", dbConnection);
            int categoryCount = (int)cmdCount.ExecuteScalar();
            Console.WriteLine("Categories count: {0} ", categoryCount);
            dbConnection.Close();
        }
    }
}
