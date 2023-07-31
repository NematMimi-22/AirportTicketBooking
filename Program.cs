using AirportTicketBooking;
using static AirportTicketBooking.Flight;
Booking bookingSystem = new Booking();
Console.WriteLine("Welcome to Airport Ticket Booking system");
Console.WriteLine("which type of user you are? ");
Console.WriteLine("1.Passenger");
Console.WriteLine("2.Manager");
int choice = int.Parse(Console.ReadLine()); 
if(choice == 1)
{
    Console.WriteLine("Please enter your details:");
    Console.Write("Name: ");
    string name = Console.ReadLine();
    Console.Write("Email: ");
    string email = Console.ReadLine();
    Passenger passenger = new Passenger
    {
        Name = name,
        Email = email
    };

    bool exitMenu = false;
    while (!exitMenu)
    {
        Console.WriteLine("\nPassenger Menu:");
        Console.WriteLine("1. Book a flight");
        Console.WriteLine("2. Search for available flights");
        Console.WriteLine("3. Show booked flights");
        Console.WriteLine("4. Exit");

        Console.Write("Enter your choice: ");
        int passengerChoice = int.Parse(Console.ReadLine());

        switch (passengerChoice)
        {
            case 1:
                Console.WriteLine("\nAvailable Flights:");
                foreach (var flight in bookingSystem.SearchFlights(null, null, null, null, null, null, null))
                {
                    Console.WriteLine($"Flight Code: {flight.FlightNum}, Class: {flight.Class}, Price: {flight.Price}, Departure Date: {flight.DepartureDate}");
                }

                Console.WriteLine("\nEnter the Flight number to book the flight:");
                string flightnumToBook = Console.ReadLine();
                Flight selectedFlight = bookingSystem.flights.Find(flight => flight.FlightNum == flightnumToBook);
                if (selectedFlight != null)
                {
                    bookingSystem.BookFlight(passenger, selectedFlight);
                    Console.WriteLine("Flight successfully booked!");
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

                List<Flight> availableFlights = bookingSystem.SearchFlights(maxPrice, null, null, null, null, null, flightClass);

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
                    Console.WriteLine($"Flight Code: {booking.FlightNum}, Class: {booking.Class}, Price: {booking.Price}, Departure Date: {booking.DepartureDate}");
                }
                break;

            case 4:
                exitMenu = true;
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}
else if(choice == 2)
{
    //manager code,,,
}






