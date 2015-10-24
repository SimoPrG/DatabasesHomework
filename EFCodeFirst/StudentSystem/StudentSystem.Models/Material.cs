namespace StudentSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Material
    {
        public int Id { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [MaxLength(8), Required, Column(TypeName = "varchar")]
        public string FileExtension { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
