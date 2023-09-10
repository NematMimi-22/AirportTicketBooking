using AirportTicketBooking;
using AirportTicketBooking.Enum;
using AirportTicketBooking.Repositories;
using Xunit;
namespace AirportTicketBookingTest.Flights_Tests
{
    public class SearchForFlightsTests
    {
        private BookingRepository _bookingRepository;
        private List<Flight> Flights;

        public SearchForFlightsTests()
        {
            _bookingRepository = new BookingRepository();
            Flights = new List<Flight>
            {
                new Flight
            {
                FlightNum = "FL456",
                NumberOfSeats = 200,
                DepartureAirport = "LHR",
                ArrivalAirport = "JFK",
                DepartureCountry = "UK",
                DestinationCountry = "USA",
                DepartureDate = DateTime.Now.Date.AddDays(2),
                Class = FlightClass.Economy,
                Price = 150
            },
                new Flight
            {
                FlightNum = "FL456",
                NumberOfSeats = 200,
                DepartureAirport = "USA",
                ArrivalAirport = "UK",
                DepartureCountry = "UK",
                DestinationCountry = "USA",
                DepartureDate = DateTime.Now.Date.AddDays(1),
                Class = FlightClass.Business,
                Price = 150
            },
                new Flight
            {
                FlightNum = "FL456",
                NumberOfSeats = 200,
                DepartureAirport = "USA",
                ArrivalAirport = "UK",
                DepartureCountry = "USA",
                DestinationCountry = "USA",
                DepartureDate = DateTime.Now.Date.AddDays(1),
                Class = FlightClass.Economy,
                Price = 200
            }
            };
            Booking.Flights = Flights;
        }

        [Fact]
        public void SearchFlights_FilterByMaxPrice_ReturnsFlightsWithMaxPrice()
        {
            // Act
            var actualfilteredFlightsCount = _bookingRepository.SearchFlights(maxPrice: 150, null, null, null, null, null, null).Count;
            var expectedCount = 2;
            // Assert
            Assert.Equal(expectedCount, actualfilteredFlightsCount);
        }

        [Fact]
        public void SearchFlights_FilterByDepartureDate_ReturnsFlightsWithDepartureDate()
        {
            // Act
            var actualfilteredFlightsCount = _bookingRepository.SearchFlights(null, null, null, DateTime.Now.Date.AddDays(1), null, null, null).Count;
            var expectedCount = 2;

            // Assert
            Assert.Equal(expectedCount, actualfilteredFlightsCount);
        }

        [Theory]
        [InlineData(null, "USA", null, null, null, null, null, 1)]
        [InlineData(null, null, "USA", null, null, null, null, 3)]
        [InlineData(null, null, null, null, "USA", null, null, 2)]
        [InlineData(null, null, null, null, null, null, FlightClass.Economy, 2)]
        public void SearchFlights_ReturnsFilteredFlights(
           double? maxPrice,
           string? departureCountry,
           string? destinationCountry,
           DateTime? departureDate,
           string? departureAirport,
           string? arrivalAirport,
           FlightClass? flightClass,
           int expectedCount)
        {
            // Act
            var actualfilteredFlightsCount = _bookingRepository.SearchFlights(maxPrice, departureCountry, destinationCountry, departureDate, departureAirport, arrivalAirport, flightClass).Count;

            // Assert
            Assert.Equal(expectedCount, actualfilteredFlightsCount);
        }
    }
}