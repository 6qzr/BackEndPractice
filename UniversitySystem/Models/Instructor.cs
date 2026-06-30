using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversitySystem.Models
{
    [Index(nameof(email), IsUnique = true)]
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int      instructorId    { get; set; }   // System Generated

        [Required]
        [MaxLength(100)]
        public string   fullName        { get; set; }   // User Input

        [Required]
        [MaxLength(150)]
        public string   email           { get; set; }   // User Input

        [MaxLength(20)]
        public string   officeNumber    { get; set; }   // User Input

        [Required]
        public DateTime hireDate        { get; set; }   // User Input

        [Required]
        [Range(0.01, (double)decimal.MaxValue)]
        public decimal  salary          { get; set; }   // User Input

        [Required]
        [MaxLength(50)]
        public string   academicTitle   { get; set; }   // From List

        public virtual Department DepartmentHeaded { get; set; } // Navigation Property

        public virtual ICollection<Course> Courses { get; set; } // Navigation Property
    }
}
