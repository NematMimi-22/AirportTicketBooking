using AirportTicketBooking;
using AirportTicketBooking.Enum;
using AirportTicketBooking.Repositories;
using Moq;

namespace AirportTicketBookingTest.Flights_Tests
{
    public class FilterFlightsTests
    {
        [Theory]
        [InlineData("Flightnum", "FL456")]
        [InlineData("NumberOfSeats", 200)]
        [InlineData("DepartureAirport", "LHR")]
        public void Test_FilterBookings_MultipleFilters(string filterPropertyName, object filterValue)
        {
            // Arrange
            var bookingList = new Booking
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
                }
            };

            var bookingRepository = new BookingRepository();

            // Act
            var filters = new FilterOptions();
            filters.GetType().GetProperty(filterPropertyName)?.SetValue(filters, filterValue);
            var result = bookingRepository.FilterBookings(filters, bookingList);

            //Assert
            Assert.NotEmpty(result);
        }
    }
}