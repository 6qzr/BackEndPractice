using System;
using System.Collections.Generic;
using System.Text;

namespace UniversitySystem.Models
{
    public class Instructor
    {
        public int      instructorId    { get; set; }   // System Generated
        public string   fullName        { get; set; }   // User Input
        public string   email           { get; set; }   // User Input
        public string   officeNumber    { get; set; }   // User Input
        public DateTime hireDate        { get; set; }   // User Input
        public decimal  salary          { get; set; }   // User Input
        public string   academicTitle   { get; set; }   // From List
    }
}
