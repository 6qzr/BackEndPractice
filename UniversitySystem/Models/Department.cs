using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversitySystem.Models
{
    [Index(nameof(departmentName), IsUnique = true)]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int     departmentId        { get; set; }   // System Generated

        [Required]
        [MaxLength(100)]
        public  string  departmentName      { get; set; }   // User Input

        [MaxLength(50)]
        public  string  building            { get; set; }   // User Input

        [Required]
        [Range(0, double.MaxValue)]
        public  decimal budget              { get; set; }   // User Input

        [ForeignKey("Instructor")]
        public  int?    headInstructorId    { get; set; }   // Foreign Key
        public  virtual Instructor Instructor { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
