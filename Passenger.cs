namespace AirportTicketBooking
{
    public class Passenger : User
    {
        public  List<Flight> BookedFlights { get; set; } = new List<Flight>();
    }
}