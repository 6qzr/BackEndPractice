using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace UniversitySystem.Models
{
    [Index(nameof(courseCode), IsUnique = true)]
    
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int     courseId        { get; set; }   // System Generated

        [Required]
        [MaxLength(10)]
        public  string  courseCode      { get; set; }   // User Input

        [Required]
        [MaxLength(150)]
        public  string  courseTitle     { get; set; }   // User Input

        [Required]
        [Range(1, 6)]
        public  int     creditHours     { get; set; }   // User Input

        [ForeignKey("Department")]
        public  int     departmentId    { get; set; }   // Foreign Key
        public  virtual Department Department { get; set; }

        [ForeignKey("Instructor")]
        public  int?    instructorId    { get; set; }   // Foreign Key
        public  virtual Instructor Instructor { get; set; } // Navigation Property

        [Required]
        [MaxLength(20)]
        public  string  semesterOffered { get; set; }   // User Input

        public virtual ICollection<Enrollment> Enrollments { get; set; } // Navigation Property
    }
}
