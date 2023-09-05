using AirportTicketBooking;
using AirportTicketBooking.Enum;
using AirportTicketBooking.Repositories;
using Xunit;
namespace AirportTicketBookingTest.Flights_Tests
{
    public class SearchForFlightsTests
    {
        private BookingRepository _bookingRepository;
        private List<Flight> _mockFlights;

        public SearchForFlightsTests()
        {
            _bookingRepository = new BookingRepository();
            _mockFlights = new List<Flight>
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
            Booking.Flights = _mockFlights;
        }

        [Fact]
        public void SearchFlights_FilterByMaxPrice_ReturnsFlightsWithMaxPrice()
        {
            // Act
            var filteredFlights = _bookingRepository.SearchFlights(maxPrice: 150, null, null, null, null, null, null);

            // Assert
            Assert.Equal(2, filteredFlights.Count());
            Assert.True(filteredFlights.All(flight => flight.Price <= 150));
        }

        [Fact]
        public void SearchFlights_FilterByDepartureCountry_ReturnsFlightsFromUSA()
        {
            // Act
            var filteredFlights = _bookingRepository.SearchFlights(null, "USA", null, null, null, null, null);

            // Assert
            Assert.Equal("USA", filteredFlights.First().DepartureCountry);
        }

        [Fact]
        public void SearchFlights_FilterByDestinationCountry_ReturnsFlightsToUSA()
        {
            // Act
            var filteredFlights = _bookingRepository.SearchFlights(null, null, "USA", null, null, null, null);

            // Assert
            Assert.Equal("USA", filteredFlights.First().DestinationCountry);
        }

        [Fact]
        public void SearchFlights_FilterByDepartureAirport_ReturnsFlightsDepartingFromUSA()
        {
            // Act
            var filteredFlights = _bookingRepository.SearchFlights(null, null, null, null, "USA", null, null);

            // Assert
            Assert.Equal("USA", filteredFlights.First().DepartureAirport);
        }

        [Fact]
        public void SearchFlights_FilterByFlightClass_ReturnsEconomyClassFlights()
        {
            // Act
            var filteredFlights = _bookingRepository.SearchFlights(null, null, null, null, null, null, FlightClass.Economy);

            // Assert
            Assert.Equal(2, filteredFlights.Count());
            Assert.Equal(FlightClass.Economy, filteredFlights.First().Class);
        }

        [Fact]
        public void SearchFlights_FilterByDepartureDate_ReturnsFlightsWithDepartureDate()
        {
            // Act
            var filteredFlights = _bookingRepository.SearchFlights(null, null, null, DateTime.Now.Date.AddDays(1), null, null, null);

            // Assert
            Assert.Equal(2, filteredFlights.Count());
            Assert.Equal(DateTime.Now.Date.AddDays(1), filteredFlights.First().DepartureDate);
        }
    }
}