using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.Models
{
    public class Pilot
    {
        public  int    pilotId       { get; set; }  // System Generated
        public  string pilotName     { get; set; }  // User Input
        public  string pilotPhone    { get; set; }  // User Input
        public  string licenseNumber { get; set; }  // User Input
        public  int    flightHours   { get; set; }  // Calculated from flight duration
        public  bool   isAvailable   { get; set; }  // Default Value = true

    }
}
