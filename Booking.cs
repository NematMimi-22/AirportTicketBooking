using AirportTicketBooking.Repositories;
namespace AirportTicketBooking
{
    public  class Booking
    {
        public static List<Flight> Flights { get;  set; }
        public List<BookingDetails> Bookings = new List<BookingDetails>();
        private readonly IBookingRepository bookingRepository;
        public Booking(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }
    }
}