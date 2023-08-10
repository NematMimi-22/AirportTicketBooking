using AirportTicketBooking;
using AirportTicketBooking.Repositories;
using AirportTicketBooking.Enum;
public class Program
{
   public static List<Passenger> passengers = new List<Passenger>();
   public static List<Manager> managers = new List<Manager>();

   public static void Main()
    {
        Console.WriteLine("Welcome to Airport Ticket Booking system");
        Console.Write("Enter the path of the CSV file: ");
        string filePath = Console.ReadLine();
        var Bookingrepository = new BookingRepository();
        Booking bookingSystem = new Booking(Bookingrepository);
        Bookingrepository.LoadFlightsFromCsv(filePath);
        bool exitSystem = false;
        while (!exitSystem)
        {
            Console.WriteLine("\nWhich type of user are you?");
            Console.WriteLine("1. Passenger");
            Console.WriteLine("2. Manager");
            Console.WriteLine("3. Exit");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    PassengerMenu();
                    break;

                case 2:
                    ManagerMenu();
                    break;

                case 3:
                    exitSystem = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        void ManagerMenu()
        {
            Console.WriteLine("Please enter your details:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Manager manager = null;
            ManagerRepository managerRepository = new ManagerRepository();
            manager = managerRepository.GetOrCreateManager(name, email, password);
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.WriteLine("\nManager Menu:");
                Console.WriteLine("1. Filter Bookings");
                Console.WriteLine("2. Back to choose user type menu");
                Console.Write("Enter your choice: ");
                int managerChoice = int.Parse(Console.ReadLine());
                switch (managerChoice)
                {
                    case 1:
                        var filters = new FilterOptions();
                        Console.Write("Enter Departure Country: ");
                        filters.DepartureCountry = Console.ReadLine();
                        Console.Write("Enter Class (Economy, Business, First): ");
                        string classInput = Console.ReadLine();
                        if (Enum.TryParse(classInput, true, out FlightClass flightClass))
                        {
                            filters.Class = flightClass;
                        }
                        else
                        {
                            Console.WriteLine("Invalid class input. Defaulting to Economy.");
                            filters.Class = FlightClass.Economy;
                        }
                        var filteredBookings = Bookingrepository.FilterBookings(filters, bookingSystem);
                        foreach (var booking in filteredBookings)
                        {
                            Console.WriteLine("Booking Details:");
                            Console.WriteLine($"Price: {booking.Price}");
                            Console.WriteLine($"Departure Country: {booking.DepartureCountry}");
                            Console.WriteLine($"Destination Country: {booking.DestinationCountry}");
                            Console.WriteLine($"Departure Date: {booking.DepartureDate}");
                            Console.WriteLine($"Departure Airport: {booking.DepartureAirport}");
                            Console.WriteLine($"Arrival Airport: {booking.ArrivalAirport}");
                            Console.WriteLine($"Passenger Email: {booking.PassengerId}");
                            Console.WriteLine($"Class: {booking.Class}");
                            Console.WriteLine("-----------------------------------------");
                        }
                        break;

                    case 2:
                        exitMenu = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        void PassengerMenu()
        {
            Console.WriteLine("Please enter your details:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Passenger passenger = null;
            PassengerRepository passengerRepository = new PassengerRepository();
            passenger = passengerRepository.GetOrCreatePassenger(name, email);
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.WriteLine("\nPassenger Menu:");
                Console.WriteLine("1. Book a flight");
                Console.WriteLine("2. Search for available flights");
                Console.WriteLine("3. Show booked flights");
                Console.WriteLine("4. Cancel Booking");
                Console.WriteLine("5. Modify Booking");
                Console.WriteLine("6. Back to choose user type menu");
                Console.Write("Enter your choice: ");
                int passengerChoice = int.Parse(Console.ReadLine());
                var Bookingrepository = new BookingRepository();
                switch (passengerChoice)
                {
                    case 1:
                        Console.WriteLine("\nAvailable Flights:");
                        foreach (var flight in Bookingrepository.SearchFlights(null, null, null, null, null, null, null))
                        {
                            Console.WriteLine($"Flight Code: {flight.FlightNum}, Class: {flight.Class}, Price: {flight.Price}, Departure Date: {flight.DepartureDate} {flight.NumberOfSeats}");
                        }
                        Console.WriteLine("\nEnter the Flight number to book the flight:");
                        string flightnumToBook = Console.ReadLine();
                        var selectedFlight = Booking.Flights.Find(flight => flight.FlightNum == flightnumToBook);
                        if (selectedFlight != null)
                        {
                            Bookingrepository.BookFlight(passenger, selectedFlight, bookingSystem);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Flight Code. Please try again.");
                        }
                        break;

                    case 2:
                        Console.WriteLine("\nPlease enter the search criteria:");
                        Console.Write("Max Price (leave empty for any price): ");
                        double? maxPrice = null;
                        double tempMaxPrice;
                        if (double.TryParse(Console.ReadLine(), out tempMaxPrice))
                        {
                            maxPrice = tempMaxPrice;
                        }

                        Console.Write("Class (Economy, Business, FirstClass) - leave empty for any class: ");
                        string flightClassInput = Console.ReadLine();
                        if (flightClassInput == "Economy")
                        {
                            flightClassInput = "0";
                        }
                        else if (flightClassInput == "Business")
                        {
                            flightClassInput = "1";
                        }
                        else if (flightClassInput == "FirstClass")
                        {
                            flightClassInput = "2";
                        }
                        FlightClass? flightClass = null;
                        if (!string.IsNullOrEmpty(flightClassInput))
                        {
                            FlightClass parsedClass;
                            if (Enum.TryParse(flightClassInput, true, out parsedClass))
                            {
                                flightClass = parsedClass;
                            }
                            else
                            {
                                Console.WriteLine("Invalid class input. Ignoring class filter.");
                            }
                        }
                        Console.Write("Enter Departure Country (leave empty for any country): ");
                        string departureCountry = Console.ReadLine();

                        Console.Write("Enter Destination Country (leave empty for any country): ");
                        string destinationCountry = Console.ReadLine();

                        Console.Write("Enter Departure Date (leave empty for any date): ");
                        DateTime? departureDate = null;
                        DateTime tempDepartureDate;
                        if (DateTime.TryParse(Console.ReadLine(), out tempDepartureDate))
                        {
                            departureDate = tempDepartureDate;
                        }

                        Console.Write("Enter Departure Airport (leave empty for any airport): ");
                        string departureAirport = Console.ReadLine();

                        Console.Write("Enter Arrival Airport (leave empty for any airport): ");
                        string arrivalAirport = Console.ReadLine();

                        var availableFlights = Bookingrepository.SearchFlights(maxPrice, departureCountry, destinationCountry, departureDate, departureAirport, arrivalAirport, flightClass);

                        Console.WriteLine("\nAvailable Flights:");
                        foreach (var flight in availableFlights)
                        {
                            Console.WriteLine($"Flight Code: {flight.FlightNum}, Class: {flight.Class}, Price: {flight.Price}, Departure Date: {flight.DepartureDate}");
                        }

                        break;

                    case 3:
                        Console.WriteLine("\nYour Booked Flights:");
                        foreach (var booking in passenger.BookedFlights)
                        {
                            Console.WriteLine($"Flight Code: {booking.FlightNum}, Class: {booking.Class}, Price: {booking.Price}, Departure Date: {booking.DepartureDate},{booking.NumberOfSeats}");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Enter the Flight Number of the flight to cancel the booking:");
                        string flightNumToCancel = Console.ReadLine();
                        var flightToCancel = passenger.BookedFlights.FirstOrDefault(flight => flight.FlightNum == flightNumToCancel);
                        if (flightToCancel != null)
                        {
                            Bookingrepository.CancelBooking(passenger, flightToCancel, bookingSystem);
                        }
                        else
                        {
                            Console.WriteLine("Flight not found. Unable to cancel booking.");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Enter the Flight Number of the flight to modify the booking:");
                        string flightNumToCancel1 = Console.ReadLine();
                        var flightToModify = passenger.BookedFlights.FirstOrDefault(flight => flight.FlightNum == flightNumToCancel1);
                        if (flightToModify != null)
                        {
                            Bookingrepository.ModifyBooking(flightToModify, Bookingrepository, passenger);
                        }
                        else
                        {
                            Console.WriteLine("Flight not found. Unable to modify booking.");
                        }
                        break;

                    case 6:
                        exitMenu = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}