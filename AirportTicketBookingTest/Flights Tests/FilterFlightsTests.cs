using AirportTicketBooking;
using AirportTicketBooking.Enum;
using AirportTicketBooking.Repositories;
using Xunit;
namespace AirportTicketBookingTest.Flights_Tests
{
    public class FilterFlightsTests
    {
        private Booking bookingList = new Booking
        {
            Bookings = new List<BookingDetails>
                {
                    new BookingDetails
                    {
                        PassengerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        FlightNum = "FL456",
                        NumberOfSeats = 200,
                        DepartureAirport = "LHR",
                        ArrivalAirport = "JFK",
                        DepartureCountry = "UK",
                        DestinationCountry = "USA",
                        DepartureDate = DateTime.Now.Date.AddDays(2),
                        Class = FlightClass.Economy,
                        Price = 150
                    }
                    ,
                        new BookingDetails
                        {
                            PassengerId = Guid.Parse("11225544-5566-7788-99AA-BBCCDDEEFF00"),
                            FlightNum = "FL458",
                            NumberOfSeats = 300,
                            DepartureAirport = "LHR",
                            ArrivalAirport = "JFK",
                            DepartureCountry = "UK",
                            DestinationCountry = "USA",
                            DepartureDate = DateTime.Now.Date.AddDays(8),
                            Class = FlightClass.Business,
                            Price = 50
                        }
                }
        };

        [Fact]
        public void FilterBookings_FiltersByFlightNum()
        {
            // Arrange
            var bookingRepository = new BookingRepository();

            // Act
            var filters = new FilterOptions();
            filters.FlightNum = "FL456";
            var result = bookingRepository.FilterBookings(filters, bookingList);

            // Assert        
            Assert.All(result, booking => Assert.Equal("FL456", booking.FlightNum));
        }

        [Fact]
        public void FilterBookings_FiltersByArrivalAirport()
        {
            // Arrange
            var bookingRepository = new BookingRepository();

            // Act
            var filters = new FilterOptions();
            filters.ArrivalAirport = "LHR";
            var result = bookingRepository.FilterBookings(filters, bookingList);

            // Assert
            Assert.All(result, booking => Assert.Equal("LHR", booking.DepartureAirport));
        }
    }
}