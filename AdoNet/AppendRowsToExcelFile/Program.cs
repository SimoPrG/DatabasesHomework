//Implement appending new rows to the Excel file.
namespace AppendRowsToExcelFile
{
    using System;
    using System.Data.OleDb;

    public class Program
    {
        public static void Main()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../users.xlsx; " +
                                            "Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var command = new OleDbCommand(@"INSERT INTO [users$] Values(@name, @score)", connection);
                for (int i = 0; i < 10; i++)
                {
                    command.Parameters.AddWithValue("@name", "user" + i);
                    command.Parameters.AddWithValue("@score", i * 10);
                }

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
