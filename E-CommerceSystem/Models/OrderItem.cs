using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{
    [PrimaryKey(nameof(orderId), nameof(productId))]
    public class OrderItem
    {
        [ForeignKey(nameof(Order))]
        public int orderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(Product))]
        public int productId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [Range(1, 999)]
        public int quantity { get; set; }   // User Input

        [Required]
        [Range(0.01, (double)decimal.MaxValue)]
        public decimal unitPrice { get; set; } // Calculated
    }
}
