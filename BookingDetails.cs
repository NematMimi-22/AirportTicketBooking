using System;
namespace AirportTicketBooking
{
    public class BookingDetails : Flight
    {
        public Guid PassengerId { get; set; }
    }
}