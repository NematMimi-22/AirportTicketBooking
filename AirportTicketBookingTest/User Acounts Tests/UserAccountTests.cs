using AirportTicketBooking.Repositories;
using Xunit;
namespace AirportTicketBookingTest.Manager_Tests
{
    public class UserAccountTests
    {
        [Theory]
        [InlineData("Nemat Mimi", "nematmimi01@gmail.com", "nemat123")]
        public void GetOrCreateManager_ExistingManager_ReturnsExistingManager(string name, string email,string password)
        {
            // Arrange
            var repository = new ManagerRepository();

            // Act
            var ManagerAcount = repository.GetOrCreateManager(name, email, password);

            // Assert
            Assert.NotNull(ManagerAcount);
            Assert.Equal(name, ManagerAcount.Name);
            Assert.Equal(email, ManagerAcount.Email);
        }

        [Theory]
        [InlineData("Nemat Mimi", "nematmimi01@gmail.com")]
        public void GetOrCreatePassenger_ExistingPassenger_ReturnsPassenger(string name, string email)
        {
            // Arrange
            var repository = new PassengerRepository();

            // Act
            var ManagerAcount = repository.GetOrCreatePassenger(name, email);

            // Assert
            Assert.NotNull(ManagerAcount);
            Assert.Equal(name, ManagerAcount.Name);
            Assert.Equal(email, ManagerAcount.Email);
        }
    }
}