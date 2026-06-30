using System;
using System.Collections.Generic;
using System.Text;

namespace UniversitySystem.Models
{
    public class Student
    {
        public  int      studentId      { get; set; }   // System Generated
        public  string   fullName       { get; set; }   // User Input
        public  string   email          { get; set; }   // User Input
        public  string   phoneNumber    { get; set; }   // User Input
        public  DateTime dateOfBirth    { get; set; }   // User Input
        public  int      enrollmentYear { get; set; }   // User Input
        public  decimal  gpa            { get; set; }   // User Input
    }
}
