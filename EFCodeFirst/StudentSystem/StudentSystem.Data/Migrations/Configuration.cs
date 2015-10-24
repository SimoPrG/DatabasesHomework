namespace StudentSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using StudentSystem.Models;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemDbContext";
        }

        protected override void Seed(StudentSystemDbContext context)
        {
            //context.Courses.AddOrUpdate(c => c.Name,
            //    new Course
            //        {
            //            Name = "Databases",
            //            Description = "A database is a collection of information that is organized so that it can easily be accessed, managed, and updated. In one view, databases can be classified according to types of content: bibliographic, full-text, numeric, and images.",
            //        },
            //    new Course
            //        {
            //            Name = "Data Structures and Algorithms",
            //            Description = "Data Structures + Algorithms = Mind Blown (or JustAI)"
            //        });

            //context.Students.AddOrUpdate( s => s.FirstName,
            //    new Student
            //        {
            //            FirstName = "Gosho",
            //            MiddleName = "P",
            //            LastName = "Georgiev",
            //            StudentNumber = "12345678",
            //            Sex = Sex.Male
            //        }
            //    );
        }
    }
}
