using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{
    [Table("Categories")]
    [Index(nameof(categoryName), IsUnique = true)]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int     categoryId      { get; set; }   // System Generated

        [Required]
        [MaxLength(100)]
        public  string  categoryName    { get; set; }   // User Input

        [MaxLength(500)]
        public  string? description     { get; set; }   // User Input

        [MaxLength(300)]
        public  string? imageUrl        { get; set; }   // User Input


        public virtual ICollection<Product> Products { get; set; }
    }
}
