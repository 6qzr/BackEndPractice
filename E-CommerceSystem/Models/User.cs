using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_CommerceSystem.Models
{
    [Index(nameof(username), IsUnique = true)]
    [Index(nameof(email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int         userId              { get; set; }   // System Generated

        [Required]
        [MaxLength(50)]
        public  string      username            { get; set; }   // User Input

        [Required]
        [MaxLength(150)]
        public  string      email               { get; set; }   // User Input

        [Required]
        [MaxLength(256)]
        public  string      passwordHash        { get; set; }   // Calculated

        [Required]
        [MaxLength(100)]
        public  string      fullName            { get; set; }   // User Input

        [MaxLength(20)]
        public  string?     phoneNumber         { get; set; }   // User Input

        [MaxLength(300)]
        public  string?     address             { get; set; }   // User Input

        [Required]
        public  DateTime    registerationDate   { get; set; } = DateTime.Now;   // Default Value
        
        public  bool        isActive            { get; set; } = true;   // Default Value
        

        public  virtual     ICollection<Order>  Orders  { get; set; }   // Naviagtion Property
        public  virtual     ICollection<Review> Reviews { get; set; }   // Navigation Property
    }
}
