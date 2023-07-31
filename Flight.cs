using System;
using System.Collections.Generic;
namespace AirportTicketBooking
{
    public class Flight
    {
        public enum FlightClass
        {
            Economy=0,
            Business=1,
            FirstClass=2,
        }
        public string FlightNum { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public FlightClass Class { get; set; }
        public double Price { get; set; }

    }
}
