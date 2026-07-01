using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceSystem.Models
{
    public class Review
    {
        public  int         reviewId    { get; set; }   // System Generated
        public  int         userId      { get; set; }   // Foreign Key
        public  int         productId   { get; set; }   // Foreign Key
        public  int         rating      { get; set; }   // User Input
        public  string      comment     { get; set; }   // User Input
        public  DateTime    reviewDate  { get; set; } = DateTime.Now  // Default Value
    }
}
