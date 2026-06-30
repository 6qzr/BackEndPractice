using System;
using System.Collections.Generic;
using System.Text;

namespace UniversitySystem.Models
{
    public class Course
    {
        public  int     courseId        { get; set; }   // System Generated
        public  string  courseCode      { get; set; }   // User Input
        public  string  courseTitle     { get; set; }   // User Input
        public  int     creditHours     { get; set; }   // User Input
        public  int     departmentId    { get; set; }   // Foreign Key
        public  int?    instructorId    { get; set; }   // Foreign Key
        public  string  semesterOffered { get; set; }   // User Input
    }
}
