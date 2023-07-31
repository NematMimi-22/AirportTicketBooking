using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class Flight
    {
        public string FlightNum { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal EconomyClassPrice { get; set; }
        public decimal BusinessClassPrice { get; set; }
        public decimal FirstClassPrice { get; set; }
    }
}
