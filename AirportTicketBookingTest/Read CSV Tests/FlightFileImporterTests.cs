using AirportTicketBooking;
using Xunit;
namespace AirportTicketBookingTest.Read_CSV_Tests
{
    public class FlightFileImporterTests
    {
        [Fact]
        public void ImportFlightsFromCsv_ValidFile_ReturnsFlights()
        {
            // Arrange
            var testFilePath = "C:\\Users\\Nemat\\Desktop\\Training\\test.csv";
            var importer = new FlightFileImporter(testFilePath);

            // Act
            var flights = importer.ImportFlightsFromCsv();

            // Assert
            Assert.NotNull(flights);
        }

        [Fact]
        public void ImportFlightsFromCsv_FileNotFound_ReturnsEmptyList()
        {
            // Arrange
            var nonExistentFilePath = "C:\\Users\\Nemat\\Desktop\\Training\\invalid.csv";
            var importer = new FlightFileImporter(nonExistentFilePath);

            // Act
            var flights = importer.ImportFlightsFromCsv();

            // Assert
            Assert.Empty(flights);
        }
    }
}