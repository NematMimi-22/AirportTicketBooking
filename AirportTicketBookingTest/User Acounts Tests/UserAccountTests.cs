using AirportTicketBooking.Repositories;

namespace AirportTicketBookingTest.Manager_Tests
{
    public class UserAccountTests
    {
        [Fact]
        public void GetOrCreateManager_ExistingManager_ReturnsExistingManager()
        {
            // Arrange
            var repository = new ManagerRepository();
            var name = "Nemat Mimi";
            var email = "nematmimi01@gmail.com";
            var password = "nemat123";

            // Act
            var ManagerAcount = repository.GetOrCreateManager(name, email, password);

            // Assert
            Assert.NotNull(ManagerAcount);
            Assert.Equal(name, ManagerAcount.Name);
            Assert.Equal(email, ManagerAcount.Email);
        }

        [Fact]
        public void GetOrCreatePassenger_ExistingPassenger_ReturnsPassenger()
        {
            // Arrange
            var repository = new PassengerRepository();
            var name = "Nemat Mimi";
            var email = "nematmimi01@gmail.com";

            // Act
            var ManagerAcount = repository.GetOrCreatePassenger(name, email);

            // Assert
            Assert.NotNull(ManagerAcount);
            Assert.Equal(name, ManagerAcount.Name);
            Assert.Equal(email, ManagerAcount.Email);
        }
    }
}