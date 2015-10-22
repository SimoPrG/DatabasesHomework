// Write a method that adds a new product in the products table in the Northwind database.
// Use a parameterized SQL command.
namespace AddNewProduct
{
    using System;
    using System.Data.SqlClient;

    public class Program
    {
        public static void Main()
        {
            SqlConnection dbCon = new SqlConnection("Server=.; " + "Database=Northwind; Integrated Security=true");

            dbCon.Open();
            int newProjectId = InsertProduct(dbCon, "New product", 20, 1, "150 ml", 3.3m, 500, 400, 20, false);
            Console.WriteLine("Inserted new product with ProductID = {0}", newProjectId);

            dbCon.Close();
        }

        private static int InsertProduct(
                            SqlConnection dbCon,
                            string productName,
                            int supplierId,
                            int categoryId,
                            string quantityPerUnit,
                            decimal unitPrice,
                            int unitsInStock,
                            int unitsOnOrder,
                            int reorderLevel,
                            bool discontinued)
        {
            SqlCommand cmdInsertProject = new SqlCommand(
                "INSERT INTO Products([ProductName], [SupplierID], [CategoryID], [QuantityPerUnit]" +
                ",[UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued]) " +
                "VALUES (@productName, @supplierId, @categoryId, @quantityPerUnit, " +
                "@unitPrice, @unitsInStock, @unitsOnOrder, @reorderLevel, @discontinued)", dbCon);
            cmdInsertProject.Parameters.AddWithValue("@productName", productName);
            cmdInsertProject.Parameters.AddWithValue("@supplierId", supplierId);
            cmdInsertProject.Parameters.AddWithValue("@categoryId", categoryId);
            cmdInsertProject.Parameters.AddWithValue("@quantityPerUnit", quantityPerUnit);
            cmdInsertProject.Parameters.AddWithValue("@unitPrice", unitPrice);
            cmdInsertProject.Parameters.AddWithValue("@unitsInStock", unitsInStock);
            cmdInsertProject.Parameters.AddWithValue("@unitsOnOrder", unitsOnOrder);
            cmdInsertProject.Parameters.AddWithValue("@reorderLevel", reorderLevel);
            cmdInsertProject.Parameters.AddWithValue("@discontinued", discontinued ? 1 : 0);
            cmdInsertProject.ExecuteNonQuery();

            SqlCommand cmdSelectIdentity = new SqlCommand("SELECT @@Identity", dbCon);
            int insertedRecordId = (int)(decimal)cmdSelectIdentity.ExecuteScalar();
            return insertedRecordId;
        }
    }
}
