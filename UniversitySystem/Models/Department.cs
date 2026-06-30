using System;
using System.Collections.Generic;
using System.Text;

namespace UniversitySystem.Models
{
    public class Department
    {
        public  int     departmentId        { get; set; }   // System Generated
        public  string  departmentName      { get; set; }   // User Input
        public  string  building            { get; set; }   // User Input
        public  decimal budget              { get; set; }   // User Input
        public  int     headInstructorId    { get; set; }   // Foreign Key
    }
}
