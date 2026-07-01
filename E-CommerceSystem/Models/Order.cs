using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceSystem.Models
{
    public class Order
    {
        public  int         orderId         { get; set; }   // System Generated
        public  int         userId          { get; set; }   // Foreign Key
        public  DateTime    orderDate       { get; set; }   // System Generated
        public  decimal     totalAmount     { get; set; } = 0;  // Calculated
        public  string      status          { get; set; } = "Pending";   // From List
        public  string      shippingAddress { get; set; }   // User Input
        public  string      paymentMethod   { get; set; }   // From List
    }
}
