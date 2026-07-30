using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int     reviewId { get; set; }   // System Generated

        [ForeignKey(nameof(User))]
        public  int     userId { get; set; }   // Foreign Key
        public  virtual User User { get; set; }  // Navigation Property

        [ForeignKey(nameof(Product))]
        public  int     productId { get; set; }   // Foreign Key
        public  virtual Product Product { get; set; }    // Navigation Property

        [Required]
        [Range(1, 5)]
        public  int     rating { get; set; }   // User Input

        [MaxLength(1000)]
        public  string? comment { get; set; }   // User Input

        [Required]
        public  DateTime    reviewDate { get; set; } = DateTime.Now;   // Default Value
    }
}