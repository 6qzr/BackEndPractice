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
                    Console.ResetColor();
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
                if (!int.TryParse(Console.ReadLine().Trim(), out int aircraftSeats) || aircraftSeats <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid seats. Must be a positive number. Press Enter");
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
        
        public static void RegisterPilot()
        {
            try
            {
                DisplayHeader("Register a Pilot");

                Console.Write("\n  Enter Pilot Name: ");
                string pilotName = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(pilotName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid pilot name. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter Pilot Phone: ");
                string pilotPhone = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(pilotPhone))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid pilot phone. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter License Number: ");
                string pilotLicense = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(pilotLicense))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid license number. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                if (context.Pilots.Any(p => p.licenseNumber == pilotLicense))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  A pilot with this license number already exists. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                int pilotId = 1;
                // Get greatest Pilot ID and add 1
                if (context.Pilots.Count > 0)
                {
                    pilotId = context.Pilots.Max(p => p.pilotId) + 1;
                }

                context.Pilots.Add(new Pilot
                {
                    pilotId = pilotId,
                    pilotName = pilotName,
                    pilotPhone = pilotPhone,
                    licenseNumber = pilotLicense,
                    flightHours = 0,
                    isAvailable = true
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  Pilot Added Successfully.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"  Pilot ID = {pilotId}");
                Console.WriteLine($"  Pilot Status: Available");
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

        public static void ViewAllFlights()
        {
            try
            {
                DisplayHeader("View All Flights");

                if (context.Flights.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  No flights have been scheduled yet. Press Enter");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                foreach (var flight in context.Flights)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n  Flight Code: {flight.flightCode}");
                    Console.ResetColor();
                    Console.WriteLine($"  Route: {flight.origin} => {flight.destination}");
                    Console.WriteLine($"  Departure: {flight.departureDate} {flight.departureTime}");
                    Console.WriteLine($"  Ticket Price: {flight.ticketPrice:F2} OMR");
                    Console.WriteLine($"  Available Seats: {flight.availableSeats}");

                    Console.ForegroundColor = flight.status switch
                    {
                        Constants.FlightScheduled => ConsoleColor.Green,
                        Constants.FlightDeparted => ConsoleColor.Blue,
                        Constants.FlightCancelled => ConsoleColor.Red,
                        _ => ConsoleColor.White
                    };
                    Console.WriteLine($"  Status: {flight.status}");
                    Console.ResetColor();

                    Console.WriteLine("  -----------------------------------------");
                }

                Console.WriteLine($"\n  Total Flights: {context.Flights.Count}");
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

        public static void ScheduleFlight()
        {
            try
            {
                DisplayHeader("Schedule A Flight");

                List<Aircraft> aircrafts = context.Aircrafts
                                  .Where(a => a.isOperational)
                                  .ToList();

                if (aircrafts.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  No operational aircraft available. Cannot schedule a flight. Press Enter.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  {"ID",-5} {"Model",-20} {"Seats",-8}");
                Console.WriteLine("  -----------------------------------------");
                Console.ResetColor();
                foreach (var a in aircrafts)
                {
                    Console.WriteLine($"  {a.aircraftId,-5} {a.model,-20} {a.totalSeats,-8}");
                }

                Console.Write("\n  Select an Aircraft ID: ");
                if (!int.TryParse(Console.ReadLine().Trim(), out int selectedAircraft) || selectedAircraft <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid aircraft ID. Must be a positive number. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Aircraft aircraft = context.Aircrafts.FirstOrDefault(a => a.aircraftId == selectedAircraft);
                if (aircraft == null || !aircraft.isOperational)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid aircraft ID. Aircraft does not exist or is not operational. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                List<Pilot> pilots = context.Pilots
                            .Where(p => p.isAvailable)
                            .ToList();

                if (pilots.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  No available pilots. Cannot schedule a flight. Press Enter.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  {"ID",-5} {"Name",-20} {"Hours",-8}");
                Console.WriteLine("  -----------------------------------------");
                Console.ResetColor();
                foreach (var p in pilots)
                {
                    Console.WriteLine($"  {p.pilotId,-5} {p.pilotName,-20} {p.flightHours,-8}");
                }

                Console.Write("\n  Select a Pilot ID: ");
                if (!int.TryParse(Console.ReadLine().Trim(), out int selectedPilot) || selectedPilot <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid pilot ID. Must be a positive number. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == selectedPilot);
                if (pilot == null || !pilot.isAvailable)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid pilot ID. Pilot does not exist or is not available. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter Flight Origin: ");
                string flightOrigin = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(flightOrigin))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid flight origin. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter Flight Destination: ");
                string flightDestination = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(flightDestination))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid flight destination. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter Flight Departure Date (yyyy-MM-dd HH:mm:ss): ");
                if (!DateTime.TryParse(Console.ReadLine().Trim(), out DateTime departureDateTime))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid departure date. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }
               
                Console.Write("\n  Enter Flight Duration: ");
                if (!double.TryParse(Console.ReadLine().Trim(), out double flightDuration))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid flight duration. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter Ticket Price: ");
                if (!decimal.TryParse(Console.ReadLine().Trim(), out decimal ticketPrice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid ticket price. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                int flightId = 1;
                if (context.Flights.Count > 0)
                {
                    flightId = context.Flights.Max(f => f.flightId) + 1;
                }
                string flightCode = $"OA-{flightId}";
                context.Flights.Add(new Flight
                {
                    flightId = flightId,
                    flightCode = flightCode,
                    aircraftId = aircraft.aircraftId,
                    pilotId = selectedPilot,
                    origin = flightOrigin,
                    destination = flightDestination,
                    departureDate = departureDateTime.ToString("yyyy-MM-dd"),
                    departureTime = departureDateTime.ToString("HH:mm:ss"),
                    flightDuration = flightDuration,
                    ticketPrice = ticketPrice,
                    availableSeats = aircraft.totalSeats,
                    status = Constants.FlightScheduled,
                });

                // Update Pilot Availability
                pilot.isAvailable = false;
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  Flight Scheduled Successfully.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"  Flight ID = {flightId}");
                Console.WriteLine($"  Flight Code = {flightCode}");
                Console.WriteLine($"  Flight Status: Scheduled");
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
 

        public static void BookFlight()
        {
            try
            {
                DisplayHeader("Book a Flight");

                Console.Write("\n  Enter your Passenger's ID: ");
                if (!int.TryParse(Console.ReadLine().Trim(), out int passengerId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid passenger ID. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                if(!context.Passengers.Any(p => p.passengerId == passengerId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid passenger ID. No passenger with such an ID. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\n  Enter the destination: ");
                string destination = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(destination))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid destination. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                List<Flight> flights = context.Flights
                                .Where(f => f.destination.ToLower() == destination.ToLower()
                                            && f.status == Constants.FlightScheduled
                                            && f.availableSeats > 0)
                                .ToList();

                if (flights.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n  No available flights to {destination}. Press Enter.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  {"ID",-5} {"Code",-8} {"Origin",-15} {"Date",-12} {"Time",-10} {"Seats",-7} {"Price",-10}");
                Console.WriteLine("  -----------------------------------------------------------------------");
                Console.ResetColor();
                foreach (var f in flights)
                {
                    Console.WriteLine($"  {f.flightId,-5} {f.flightCode,-8} {f.origin,-15} {f.departureDate,-12} {f.departureTime,-10} {f.availableSeats,-7} {f.ticketPrice,-8:F2} OMR");
                }

                Console.Write("\n  Select a Flight ID: ");
                if (!int.TryParse(Console.ReadLine().Trim(), out int selectedFlightId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid flight ID. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Flight flight = flights.FirstOrDefault(f => f.flightId == selectedFlightId);
                if (flight == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid flight ID. Flight not found in the list above. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                int bookingId = 1;
                if (context.Bookings.Count > 0)
                {
                    bookingId = context.Bookings.Max(b => b.bookingId) + 1;
                }

                string seatNumber = $"S{flight.flightId}-{bookingId}";

                context.Bookings.Add(new Booking
                {
                    bookingId = bookingId,
                    passengerId = passengerId,
                    flightId = flight.flightId,
                    seatNumber = seatNumber,
                    bookingDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    totalPrice = flight.ticketPrice,
                    status = Constants.BookingConfirmed
                });

                // Updaete Flight's available seats
                flight.availableSeats -= 1;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  Booking Confirmed Successfully.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"  Booking ID = {bookingId}");
                Console.WriteLine($"  Seat Number = {seatNumber}");
                Console.WriteLine($"  Total Price = {flight.ticketPrice:F2} OMR");
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
                        RegisterPilot();
                        break;
                    case "4":
                        ViewAllFlights();
                        break;
                    case "5":
                        ScheduleFlight();
                        break;
                    case "6":
                        BookFlight();
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
