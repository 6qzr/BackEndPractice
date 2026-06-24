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

        public static void MainMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=========================================");
                Console.WriteLine("       FLIGHT MANAGEMENT SYSTEM");
                Console.WriteLine("=========================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n1. Register a Passenger");
                Console.WriteLine("2. Add an Aircraft");
                Console.WriteLine("3. Register a Pilot");
                Console.WriteLine("4. View All Flights");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n5. Schedule a Flight");
                Console.WriteLine("6. Book a Flight");
                Console.WriteLine("7. Cancel a Booking");
                Console.WriteLine("8. Depart a Flight");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n9. Cancel a Flight");
                Console.WriteLine("10. Passenger Booking History");
                Console.WriteLine("11. Flight Revenue & Load Factor Report");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n0. Exit");
                Console.ResetColor();

                Console.Write("\nChoose an option: ");
                string input = Console.ReadLine().Trim();

                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        //RegisterPassenger();
                        break;
                    case "2":
                        //AddAircraft();
                        break;
                    case "3":
                        //RegisterPilot();
                        break;
                    case "4":
                        //ViewAllFlights();
                        break;
                    case "5":
                        //ScheduleFlight();
                        break;
                    case "6":
                        //BookFlight();
                        break;
                    case "7":
                        //CancelBooking();
                        break;
                    case "8":
                        //DepartFlight();
                        break;
                    case "9":
                        //CancelFlight();
                        break;
                    case "10":
                        //PassengerBookingHistory();
                        break;
                    case "11":
                        //FlightRevenueReport();
                        break;
                    case "0":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Goodbye!");
                        Console.ResetColor();
                        running = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
                            }
        }

        static void Main(string[] args)
        {
            MainMenu();
        }
    }
}
