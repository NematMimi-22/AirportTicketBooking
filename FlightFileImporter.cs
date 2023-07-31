using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class FlightFileImporter
    {
        List<Flight> flights = new List<Flight>();
        public List<Flight> ImportFlightsFromCsv(string filePath)
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
