using static AirportTicketBooking.Flight;

namespace AirportTicketBooking
{
    internal class Booking
    {
        public List<Flight> flights;
        private List<Flight> bookings;

        public Booking()
        {
            flights = FlightFileImporter.ImportFlightsFromCsv("C:\\Users\\Nemat\\Desktop\\test.csv");
            bookings = new List<Flight>(); // Initialize the bookings list in the constructor.
        }

        public void BookFlight(Passenger passenger, Flight flight)
        {
          passenger.BookedFlights.Add(flight);
        }

        public void ViewBookings(Passenger passenger)
        {
            if (passenger.BookedFlights.Count == 0)
            {
                Console.WriteLine("No flights booked yet.");
            }
            else
            {
                Console.WriteLine("Your Booked Flights:");
                foreach (var booking in passenger.BookedFlights)
                {
                    Console.WriteLine($"Flight Code: {booking.FlightNum}, Class: {booking.Class}, Price: {booking.Price}, Departure Date: {booking.DepartureDate}");
                }
            }
        }


        public List<Flight> SearchFlights(
                   double? maxPrice,
                   string departureCountry,
                   string destinationCountry,
                   DateTime? departureDate,
                   string departureAirport,
                   string arrivalAirport,
                   FlightClass? flightClass)
        {
            return flights
                .Where(flight =>
                    (maxPrice == null || flight.Price <= maxPrice)
                    && (string.IsNullOrEmpty(departureCountry) || flight.DepartureCountry == departureCountry)
                    && (string.IsNullOrEmpty(destinationCountry) || flight.DestinationCountry == destinationCountry)
                    && (!departureDate.HasValue || flight.DepartureDate.Date == departureDate.Value.Date)
                    && (string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport == departureAirport)
                    && (string.IsNullOrEmpty(arrivalAirport) || flight.ArrivalAirport == arrivalAirport)
                    && (!flightClass.HasValue || flight.Class == flightClass))
                .ToList();
        }

        public void CancelBooking(Passenger passenger, Flight flight)
        {
            passenger.BookedFlights.Remove(flight);
            Console.WriteLine("Booking canceled successfully!");
        }

    }
}
