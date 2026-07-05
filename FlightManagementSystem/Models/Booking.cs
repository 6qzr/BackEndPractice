using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.Models
{
    public class Booking
    {
        public  int     bookingId   { get; set; }   // System Generated
        public  int     passengerId { get; set; }   // From List
        public  int     flightId    { get; set; }   // From List
        public  string  seatNumber  { get; set; }   // System Generated
        public  string  bookingDate { get; set; }   // System Generated
        public  decimal totalPrice  { get; set; }   // Calculated from Flight ticketPrice
        public  string  status      { get; set; }   // Default Value
    }
}
