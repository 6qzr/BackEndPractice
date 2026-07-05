using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.Models
{
    public class Passenger
    {
        public  int    passengerId    { get; set; }  // System Generated
        public  string passengerName  { get; set; }  // User Input
        public  string passengerEmail { get; set; }  // User Input
        public  string passengerPhone { get; set; }  // User Input
        public  string passportNumber { get; set; }  // User Input
        public  string nationality    { get; set; }  // User Input
    }
}
