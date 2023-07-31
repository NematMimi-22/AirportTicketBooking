// See https://aka.ms/new-console-template for more information
using AirportTicketBooking;

string filePath = "C:\\Users\\Nemat\\Desktop\\test.csv";

FlightFileImporter flightImporter = new FlightFileImporter();

List<Flight> flights = flightImporter.ImportFlightsFromCsv(filePath);
