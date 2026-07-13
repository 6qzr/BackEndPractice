using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }   // System Generated

        [Required]
        [MaxLength(150)]
        public string productName { get; set; }   // User Input

        [MaxLength(1000)]
        public string? description { get; set; }   // User Input

        [Required]
        [Range(0.01, (double)decimal.MaxValue)]
        public decimal price { get; set; }   // User Input

        [Range(0, int.MaxValue)]
        public int stockQuantity { get; set; } = 0;   // Default Value

        [MaxLength(300)]
        public string? imageUrl { get; set; }   // User Input

        [ForeignKey(nameof(Category))]
        public int categoryId { get; set; }   // Foreign Key
        public virtual Category Category { get; set; }  // Navigation Property

        [Required]
        public DateTime createdAt { get; set; } = DateTime.Now;   // Default Value

        public bool isAvailable { get; set; } = true;   // Default Value

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}