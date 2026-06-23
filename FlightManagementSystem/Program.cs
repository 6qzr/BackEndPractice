using FlightManagementSystem.Models;

namespace FlightManagementSystem
{
    public class Program
    {

        // System Storage
        public static FlightContext context = new FlightContext
        {
            Passengers = new List<Passenger>(),
            Pilots     = new List<Pilot>(),
            Aircrafts  = new List<Aircraft>(),
            Flights    = new List<Flight>(),
            Bookings   = new List<Booking>()
        };

        static void Main(string[] args)
        {
            
        }
    }
}
