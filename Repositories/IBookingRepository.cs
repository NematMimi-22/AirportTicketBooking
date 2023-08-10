using AirportTicketBooking.Enum;

namespace AirportTicketBooking.Repositories
{
    public interface IBookingRepository
    {
        BookedStatus BookFlight(Passenger passenger, Flight flight, Booking bookingSystem);
        void CancelBooking(Passenger passenger, Flight flight, Booking bookingSystem);
        IEnumerable<BookingDetails> FilterBookings(FilterOptions filters, Booking bookingSystem);
        void LoadFlightsFromCsv(string filepath);
        void ModifyBooking(Flight flight, BookingRepository Bookingrepository, Passenger passenger);
        List<Flight> SearchFlights(double? maxPrice, string? departureCountry, string? destinationCountry, DateTime? departureDate, string? departureAirport, string? arrivalAirport, FlightClass? flightClass);
    }
}