using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceSystem.Models
{
    public class User
    {
        public  int         userId              { get; set; }   // System Generated
        public  string      username            { get; set; }   // User Input
        public  string      email               { get; set; }   // User Input
        public  string      passwordHash        { get; set; }   // User Input
        public  string      fullName            { get; set; }   // User Input
        public  string      phoneNumber         { get; set; }   // User Input
        public  string      address             { get; set; }   // User Input
        public  DateTime    registerationDate   { get; set; } = DateTime.Now   // Default Value
        public  bool        isActive            { get; set; } = true;  // Default Value          
    }
}
