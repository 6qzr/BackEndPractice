using System;
using System.Collections.Generic;
using System.Text;

namespace UniversitySystem.Models
{
    public class Enrollment
    {
        public  int         enrollmentId    { get; set; }   // System Generated
        public  int         studentId       { get; set; }   // Foreign Key
        public  int         courseId        { get; set; }   // Foreign Key
        public  DateTime    enrollmentDate  { get; set; }   // User Input
        public  string?     finalGrade      { get; set; }   // User Input
        public  string      status          { get; set; }   // Default Value
    }
}
