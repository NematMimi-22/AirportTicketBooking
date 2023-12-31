﻿using AirportTicketBooking.Enum;
namespace AirportTicketBooking
{

    public class FilterOptions
    {
        public string?  FlightNum { get; set; }
        public decimal? Price { get; set; }
        public string? DepartureCountry { get; set; }
        public string? DestinationCountry { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public Guid? PassengerId { get; set; }
        public FlightClass? Class { get; set; }
    }
}