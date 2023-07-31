using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace AirportTicketBooking
{
    public static class FlightFileImporter
    {
       static List<Flight> flights = new List<Flight>();
        public static List<Flight> ImportFlightsFromCsv(string filePath)
        {
            List<Flight> flights = new List<Flight>();

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    flights = csv.GetRecords<Flight>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return flights;
        }
    }
}
