﻿// Write a program that retrieves the images for all categories in the Northwind 
// database and stores them as JPG files in the file system.
namespace RetrieveImagesFromDatabase
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    public class Program
    {
        public static void Main()
        {
            ExtractImagesFromDB(
                                "Data Source=.; Initial Catalog = Northwind; Integrated Security = SSPI",
                                "SELECT CategoryName, Picture FROM Categories",
                                "CategoryName",
                                "Picture",
                                ".jpg",
                                "../../");
        }

        private static void WriteBinaryFile(string fileName, int offset, byte[] fileContents)
        {
            FileStream stream = File.OpenWrite(fileName);
            using (stream)
            {
                stream.Write(fileContents, offset, fileContents.Length - offset);
            }
        }

        private static void ExtractImagesFromDB(
                                                string connectionString,
                                                string query,
                                                string nameColumn,
                                                string pictureColumn,
                                                string format,
                                                string path)
        {
            SqlConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            SqlCommand cmd = new SqlCommand(query, dbConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    var name = (string)reader[nameColumn];
                    var escapedName = name.Replace(@"/", @"-");
                    var picture = (byte[])reader[pictureColumn];
                    Console.WriteLine("Creating " + escapedName + format);
                    WriteBinaryFile(path + escapedName + format, 78, picture);
                }
            }
            dbConnection.Close();
        }
    }
}
