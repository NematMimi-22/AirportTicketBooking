namespace AirportTicketBooking.Repositories
{
    public class PassengerRepository
    {
        public Passenger GetOrCreatePassenger(string name, string email)
        {
            var existingPassenger = Program.passengers.FirstOrDefault(p => p.Name == name && p.Email == email);
            if (existingPassenger != null)
            {
                Console.WriteLine($"Welcome back, {existingPassenger.Name}!");
                return existingPassenger;
            }
            else
            {
                var newPassenger = new Passenger
                {
                    Name = name,
                    Email = email
                };
                Program.passengers.Add(newPassenger);
                Console.WriteLine($"Welcome, {newPassenger.Name}!");
                return newPassenger;
            }
        }
        public void UpdateBookedFlights(Passenger passenger, Flight oldFlight, Flight newFlight)
        {
            if (passenger.BookedFlights.Contains(oldFlight))
            {
                passenger.BookedFlights.Remove(oldFlight);
                passenger.BookedFlights.Add(newFlight);
            }
        }
    }
}