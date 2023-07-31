namespace AirportTicketBooking
{
    internal class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Flight> BookedFlights { get; set; }

        public Passenger()
        {
            BookedFlights = new List<Flight>();
        }
    }
}
