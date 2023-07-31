using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{ 
public enum FlightClass
{
    Economy,
    Business,
    FirstClass
}


    internal class Booking
    {


        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public FlightClass FlightClass { get; set; }
        public decimal Price { get; set; }

    }
}
