using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; }   // System Generated

        [ForeignKey(nameof(User))]
        public int userId { get; set; }   // Foreign Key
        public virtual User User { get; set; }  // Naviagtion Property

        [Required]
        public DateTime orderDate { get; set; } = DateTime.Now;   // Default Value

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal totalAmount { get; set; } = 0;   // Default Value

        [Required]
        [MaxLength(30)]
        public string status { get; set; } = "Pending";   // default value

        [Required]
        [MaxLength(300)]
        public string shippingAddress { get; set; }   // User Input

        [Required]
        [MaxLength(50)]
        public string paymentMethod { get; set; }   // From List

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}