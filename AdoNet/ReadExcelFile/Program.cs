// Create an Excel file with 2 columns: name and score:
namespace ReadExcelFile
{
    using System;
    using System.Data.OleDb;

    public class Program
    {
        public static void Main()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../users.xlsx; " +
                                            "Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";

            OleDbConnection connection = new OleDbConnection(connectionString);

            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [users$]", connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader["Name"];
                        var score = reader["Score"];
                        Console.WriteLine("Name - {0}; Score - {1}", name, score);
                    }
                }

                connection.Close();
            }
        }
    }
}
