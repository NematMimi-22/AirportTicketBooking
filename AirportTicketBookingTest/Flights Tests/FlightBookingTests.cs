using AirportTicketBooking;
using AirportTicketBooking.Enum;
using AirportTicketBooking.Repositories;
using CsvHelper.Configuration;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;

namespace AirportTicketBookingTest
{
    public class FlightBookingTests
    {
        [Fact]
        public void BookFlight_SuccessfulBooking_ReturnsSuccessfullyBookedStatus()
        {
            // Arrange
            var passenger = new Passenger();
            var flight = new Flight { NumberOfSeats = 1 };
            var bookingSystem = new Booking();
            var bookingRepository = new BookingRepository();

            // Act
            var result = bookingRepository.BookFlight(passenger, flight, bookingSystem);

            // Assert
            Assert.Equal(BookingStatus.SuccessfullyBooked, result);
            Assert.Contains(flight, passenger.BookedFlights);
            Assert.Equal(0, flight.NumberOfSeats);
        }

        [Fact]
        public void BookFlight_AlreadyBooked_ReturnsFailedBookingStatus()
        {
            // Arrange
            var passenger = new Passenger();
            var flight = new Flight();
            passenger.BookedFlights.Add(flight);
            var bookingSystem = new Booking();
            var bookingRepository = new BookingRepository();

            // Act
            var result = bookingRepository.BookFlight(passenger, flight, bookingSystem);

            // Assert
            Assert.Equal(BookingStatus.FailedBooking, result);
        }

        [Fact]
        public void BookFlight_NoAvailableSeats_ReturnsFailedBookingStatus()
        {
            // Arrange
            var passenger = new Passenger();
            var flight = new Flight { NumberOfSeats = 0 };
            var bookingSystem = new Booking();
            var bookingRepository = new BookingRepository();

            // Act
            var result = bookingRepository.BookFlight(passenger, flight, bookingSystem);

            // Assert
            Assert.Equal(BookingStatus.FailedBooking, result);
        }

        [Fact]
        public void CancelBooking_RemovesFlightFromPassengerAndBookingSystem()
        {
            // Arrange
            var passenger = new Passenger ();
            var flight = new Flight { FlightNum = "123", DepartureDate = DateTime.Now , NumberOfSeats = 10 };
            var bookingSystem = new Booking();
            bookingSystem.Bookings.Add(new BookingDetails
            {
                PassengerId = passenger.Id,
                FlightNum = flight.FlightNum,
                DepartureDate = flight.DepartureDate,
                NumberOfSeats = flight.NumberOfSeats
            });

            // Act
            var bookingRepository = new BookingRepository();
            bookingRepository.CancelBooking(passenger, flight, bookingSystem);

            // Assert
            Assert.Equal(0,passenger.BookedFlights.Count);
            Assert.Equal(11, flight.NumberOfSeats);
        }

        [Fact]
        public void UpdateBookedFlights_ShouldNotUpdateFlight()
        {
            // Arrange
            var passenger = new Passenger();
            var oldFlight = new Flight();
            var newFlight = new Flight();
            var passengerRepository = new PassengerRepository();

            // Act
            passengerRepository.UpdateBookedFlights(passenger, oldFlight, newFlight);

            // Assert
            Assert.DoesNotContain(newFlight, passenger.BookedFlights);
        }
    }
}