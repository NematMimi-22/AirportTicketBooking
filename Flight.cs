﻿using System;
using AirportTicketBooking.Enum;
using System.Collections.Generic;
namespace AirportTicketBooking
{
    public class Flight
    {
        public string FlightNum { get; set; }
        public int NumberOfSeats { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public FlightClass Class { get; set; }
        public double Price { get; set; }
    }
}