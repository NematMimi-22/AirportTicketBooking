using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
namespace AirportTicketBooking
{
    public class FlightFileImporter
    {
        string filePath { get; }
        public FlightFileImporter(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Flight> ImportFlightsFromCsv()
        {
            var flights = new List<Flight>();

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