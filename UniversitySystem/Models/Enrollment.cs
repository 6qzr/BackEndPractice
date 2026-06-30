using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversitySystem.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int         enrollmentId    { get; set; }   // System Generated
        
        [ForeignKey("Student")]
        public  int         studentId       { get; set; }   // Foreign Key
        public  virtual Student Student     { get; set; }   // Navigation Property

        [ForeignKey("Course")]
        public  int         courseId        { get; set; }   // Foreign Key
        public  virtual     Course Course   { get; set; }   // Navigation Property

        [Required]
        public  DateTime    enrollmentDate  { get; set; }   // User Input
        
        [MaxLength(2)]
        public  string?     finalGrade      { get; set; }   // User Input
        
        [Required]
        [MaxLength(20)]
        public  string      status          { get; set; } = "In Progress";    // Default Value
    }
}
