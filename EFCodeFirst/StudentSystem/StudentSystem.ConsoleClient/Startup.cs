namespace StudentSystem.ConsoleClient
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using Data;
    using Data.Migrations;

    using StudentSystem.Models;

    class Startup
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDbContext, Configuration>());

            using (var context = new StudentSystemDbContext())
            {
                var students = new HashSet<Student>
                                   {
                                       new Student
                                           {
                                               FirstName = "Georgi",
                                               MiddleName = "P",
                                               LastName = "Georgiev",
                                               StudentNumber = "12345678"
                                           },
                                       new Student
                                           {
                                               FirstName = "Peter",
                                               MiddleName = "G",
                                               LastName = "Petrov",
                                               StudentNumber = "87654321"
                                           }
                                   };

                var course = new Course { Name = "DSA", Description = "Hold on tight!", Students = students };

                context.Courses.Add(course);
                context.SaveChanges();
            }
        }
    }
}
