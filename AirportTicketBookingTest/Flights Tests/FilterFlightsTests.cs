using AirportTicketBooking;
using AirportTicketBooking.Enum;
using AirportTicketBooking.Repositories;
using Xunit;
namespace AirportTicketBookingTest.Flights_Tests
{
    public class FilterFlightsTests
    {
        [Fact]    

        public void FilterBookings_FiltersByDate()
        {
            // Arrange
            var bookingRepository = new BookingRepository();

            // Act
            var filters = new FilterOptions();
            filters.DepartureDate = DateTime.Now.Date.AddDays(2);
            var expectedDate = DateTime.Now.Date.AddDays(2);
            var result = bookingRepository.FilterBookings(filters, bookingList);

            // Assert        
            Assert.All(result, booking => Assert.Equal(expectedDate, booking.DepartureDate));
        }

        [Fact]
        public void FilterBookings_FiltersByPassengerID()
        {
            // Arrange
            var bookingRepository = new BookingRepository();

            // Act
            var filters = new FilterOptions();
            filters.PassengerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00");

            var result = bookingRepository.FilterBookings(filters, bookingList);

            // Assert        
            Assert.All(result, booking => Assert.Equal(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), booking.PassengerId));
        }

        [Theory]
        [InlineData("FlightNum", "FL456")]
        [InlineData("DepartureCountry", "UK")]
        [InlineData("DestinationCountry", "USA")]
        [InlineData("DepartureAirport", "LHR")]
        [InlineData("ArrivalAirport", "New York")]
        [InlineData("Class", FlightClass.Economy)]
        public void FilterBookings_FiltersByDifferentTypeOfFilters(string filterPropertyName, object filterValue)
        {
            // Arrange
            var bookingRepository = new BookingRepository();

            // Act
            var filters = new FilterOptions();
            var filterProperty = typeof(FilterOptions).GetProperty(filterPropertyName);
            filterProperty.SetValue(filters, filterValue);
            var result = bookingRepository.FilterBookings(filters, bookingList);

            // Assert        
            Assert.NotEmpty(result);
        }

        public static Booking bookingList = new Booking
        {
            Bookings = new List<BookingDetails>
                {
                    new BookingDetails
                    {

                        PassengerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        FlightNum = "FL456",
                        NumberOfSeats = 200,
                        DepartureAirport = "LHR",
                        ArrivalAirport = "New York",
                        DepartureCountry = "UK",
                        DestinationCountry = "USA",
                        DepartureDate = DateTime.Now.Date.AddDays(2),
                        Class = FlightClass.Economy,
                        Price = 150
                    },
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
                    },
                }
        };
    }
}