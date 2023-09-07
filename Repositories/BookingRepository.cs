using AirportTicketBooking.Enum;
namespace AirportTicketBooking.Repositories
{
    public class BookingRepository 
    {
        public void LoadFlightsFromCsv(string filepath)
        {
            var ImportFromFile = new FlightFileImporter(filepath);
            Booking.Flights = ImportFromFile.ImportFlightsFromCsv();
        }
        public BookingStatus BookFlight(Passenger passenger, Flight flight, Booking bookingSystem)
        {
            if (flight.NumberOfSeats > 0)
            {
                if (passenger.BookedFlights.Contains(flight))
                {
                    Console.WriteLine("You have already booked this flight.");
                    return BookingStatus.FailedBooking;
                }
                passenger.BookedFlights.Add(flight);
                var bookingDetails = new BookingDetails
                {
                    FlightNum = flight.FlightNum,
                    NumberOfSeats = flight.NumberOfSeats,
                    DepartureAirport = flight.DepartureAirport,
                    ArrivalAirport = flight.ArrivalAirport,
                    DepartureCountry = flight.DepartureCountry,
                    DestinationCountry = flight.DestinationCountry,
                    DepartureDate = flight.DepartureDate,
                    Class = flight.Class,
                    Price = flight.Price,
                    PassengerId = passenger.Id
                };
                bookingSystem.Bookings.Add(bookingDetails);
                flight.NumberOfSeats = -- flight.NumberOfSeats;
                Console.WriteLine("Flight successfully booked!");
                return BookingStatus.SuccessfullyBooked;
            }
            else
            {
                Console.WriteLine("Sorry their is no avalible seats!");
                return BookingStatus.FailedBooking;
            }
        }
        public List<Flight> SearchFlights(
           double? maxPrice,
           string? departureCountry,
           string? destinationCountry,
           DateTime? departureDate,
           string? departureAirport,
           string? arrivalAirport,
           FlightClass? flightClass)
        {
            return Booking.Flights
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
        public void CancelBooking(Passenger passenger, Flight flight, Booking bookingSystem)
        {
            passenger.BookedFlights.Remove(flight);
            var bookingDetails = bookingSystem.Bookings.Find(b => b.PassengerId == passenger.Id && b.FlightNum == flight.FlightNum
            && b.DepartureDate == flight.DepartureDate && b.Class == flight.Class && b.Price == flight.Price
            && b.DestinationCountry == flight.DestinationCountry
            && b.NumberOfSeats == flight.NumberOfSeats
            && b.ArrivalAirport == flight.ArrivalAirport
            && b.DepartureAirport == flight.DepartureAirport);
            if (bookingDetails != null)
            {
                bookingSystem.Bookings.Remove(bookingDetails);
            }
            flight.NumberOfSeats = ++ flight.NumberOfSeats;
            Console.WriteLine("Booking canceled successfully!");
        }
        public void ModifyBooking(Flight flight, BookingRepository Bookingrepository, Passenger passenger)
        {
            Console.WriteLine($"Current Booking Details for Flight Code: {flight.FlightNum}");
            Console.WriteLine($"Class: {flight.Class}, Price: {flight.Price}, Departure Date: {flight.DepartureDate}");
            Console.WriteLine("\nDo you want to search for available flights on a specific date? (y/n): ");
            var searchOption = Console.ReadLine();
            if (searchOption.ToLower() == "y")
            {
                Console.Write("Enter the new departure date (MM/dd/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime inputDate))
                {
                    var availableFlights = Bookingrepository.SearchFlights(flight.Price, flight.DepartureCountry, flight.DestinationCountry, inputDate, flight.DepartureAirport, flight.ArrivalAirport, flight.Class);

                    if (availableFlights.Any())
                    {
                        Console.WriteLine("\nAvailable Flights:");
                        foreach (var availableFlight in availableFlights)
                        {
                            Console.WriteLine($"Flight Code: {availableFlight.FlightNum}, Class: {availableFlight.Class}, Price: {availableFlight.Price}, Departure Date: {availableFlight.DepartureDate}");
                        }

                        Console.Write("Enter the Flight Code of the desired flight: ");
                        var selectedFlightCode = Console.ReadLine();
                        var selectedFlight = availableFlights.FirstOrDefault(flight => flight.FlightNum == selectedFlightCode);

                        if (selectedFlight != null)
                        {
                            var passengerRepository = new PassengerRepository();
                            passengerRepository.UpdateBookedFlights(passenger, flight, selectedFlight);
                            flight.NumberOfSeats = flight.NumberOfSeats + 1;
                            selectedFlight.NumberOfSeats = selectedFlight.NumberOfSeats - 1;
                            Console.WriteLine("Booking updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Flight Number. Booking not updated.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No available flights on the specified date. Booking not updated.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Booking not updated.");
                }
            }
        }
        public IEnumerable<BookingDetails> FilterBookings(FilterOptions filters, Booking bookingSystem)
        {
            var filteredBookings = bookingSystem.Bookings.AsEnumerable();

            foreach (var filter in filters.GetType().GetProperties())
            {
                var filterValue = filter.GetValue(filters, null);
                if (filterValue != null)
                {
                    filteredBookings = filteredBookings.Where(b =>
                    {
                        var bookingValue = b.GetType().GetProperty(filter.Name)?.GetValue(b, null);
                        return bookingValue != null && bookingValue.Equals(filterValue);
                    });
                }
            }
            return filteredBookings;
        }

    }
}