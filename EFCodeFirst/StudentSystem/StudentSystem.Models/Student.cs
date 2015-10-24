namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Student
    {
        private ICollection<Course> courses;
        private ICollection<Homework> homeworks;

        public Student()
        {
            this.courses = new HashSet<Course>();
            this.homeworks = new HashSet<Homework>();
        }

        public int Id { get; set; }

        [MaxLength(64), Required]
        public string FirstName { get; set; }

        [MaxLength(64), Required]
        public string MiddleName { get; set; }

        [MaxLength(64), Required]
        public string LastName { get; set; }

        [MaxLength(8), Required, Column(TypeName = "char")]
        public string StudentNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public Sex?  Sex { get; set; }

        [MaxLength(128), Column(TypeName = "varchar")]
        public string Email { get; set; }

        [MaxLength(128), Column(TypeName = "varchar")]
        public string WebSite { get; set; }

        [MaxLength(128), Column(TypeName = "varchar")]
        public string GitHub { get; set; }

        [MaxLength(128), Column(TypeName = "varchar")]
        public string Facebook { get; set; }

        [MaxLength(128), Column(TypeName = "varchar")]
        public string LinkedIn { get; set; }

        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
