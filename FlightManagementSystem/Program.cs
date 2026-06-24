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

        public static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=========================================");
            Console.WriteLine($"       {header.ToUpper()}");
            Console.WriteLine("=========================================");
            Console.ResetColor();
        }
        
        public static void RegisterPassenger()
        {
            try
            {
                DisplayHeader("Rigester a Passenger");

                Console.Write("\n  Enter Passenger Name: ");
                string passengerName = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(passengerName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid passenger name. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter Passenger Email: ");
                string passengerEmail = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(passengerEmail))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid passenger email. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter Passenger Phone Number: ");
                string passengerPhone = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(passengerPhone))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid passenger phone. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter Passenger Passport Number: ");
                string passengerPassport = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(passengerPassport))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid passenger Passport Number. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                if (context.Passengers.Any(p => p.passportNumber == passengerPassport))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  A passenger with this passport number already exists. Press Enter");
                    Console.ReadLine();
                    return;
                }

                Console.Write("\n  Enter Passenger Nationality: ");
                string passengerNationality = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(passengerNationality))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid passenger nationality. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                int passengerId = 1;
                // Get greatest Passenger ID and add 1
                if (context.Passengers.Count > 0)
                {
                    passengerId = context.Passengers.Max(p => p.passengerId) + 1;
                }

                context.Passengers.Add(new Passenger
                {   
                    passengerId = passengerId,
                    passengerName = passengerName,
                    passengerEmail = passengerEmail,
                    passengerPhone = passengerPhone,
                    passportNumber = passengerPassport,
                    nationality = passengerNationality
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  Passenger Registered Successfully.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"  Passenger ID = {passengerId}");
                Console.ResetColor();
                Console.WriteLine("\n  Press Enter...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        public static void AddAircraft()
        {
            try
            {
                DisplayHeader("Add an Aircraft");

                Console.Write("\n  Enter Aircraft Model: ");
                string aircraftModel = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(aircraftModel))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid aircraft model. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter Total Seats: ");
                if (!int.TryParse(Console.ReadLine().Trim(), out int aircraftSeats))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid seats. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }
                
                int aircraftId = 1;
                // Get greatest Aircraft ID and add 1
                if (context.Aircrafts.Count > 0)
                {
                    aircraftId = context.Aircrafts.Max(p => p.aircraftId) + 1;
                }

                context.Aircrafts.Add(new Aircraft
                {
                    aircraftId = aircraftId,
                    model = aircraftModel,
                    totalSeats = aircraftSeats,
                    isOperational = true
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  Aircraft Added Successfully.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"  Aircraft ID = {aircraftId}");
                Console.WriteLine($"  Aircraft Status: Operational");
                Console.ResetColor();
                Console.WriteLine("\n  Press Enter...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
        
        public static void MainMenu()
        {
            bool running = true;

            while (running)
            {
                DisplayHeader("Flight Management System");

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
                        RegisterPassenger();
                        break;
                    case "2":
                        AddAircraft();
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
