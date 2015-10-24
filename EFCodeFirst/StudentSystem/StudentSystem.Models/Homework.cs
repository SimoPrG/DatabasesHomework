namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Homework
    {
        public int Id { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [MaxLength(8), Required, Column(TypeName = "varchar")]
        public string FileExtension { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime TimeSent { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }
    }
}
