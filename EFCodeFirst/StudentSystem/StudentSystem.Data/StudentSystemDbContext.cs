namespace StudentSystem.Data
{
    using System.Data.Entity;

    using StudentSystem.Models;

    public class StudentSystemDbContext : DbContext
    {
        public StudentSystemDbContext()
            : base("StudentSystem")
        {
        }

        public virtual IDbSet<Student> Students { get; set; }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Homework> Homeworks { get; set; }

        public virtual IDbSet<Material> Materials { get; set; }
    }
}
