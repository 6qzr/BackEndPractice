using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceSystem.Models
{
    public class Product
    {
        public  int         productId       { get; set; }   // System Generated
        public  string      productName     { get; set; }   // User Input
        public  string      descriptiopn    { get; set; }   // User Input
        public  decimal     price           { get; set; }   // User Input
        public  int         stockQuantity   { get; set; } = 0   // Default Value
        public  string      imageUrl        { get; set; }   // User Input
        public  int         categoryId      { get; set; }   // Foreign Key
        public  DateTime    createdAt       { get; set; }   // System Generated
        public  bool        isAvailable     { get; set; } = true;   // Default Value
    }
}
