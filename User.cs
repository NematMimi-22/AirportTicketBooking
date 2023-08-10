using System;
using AirportTicketBooking.Utilities;
namespace AirportTicketBooking
{
    public class User
    {
        private static int _idCounter = 0;
        public Guid Id { get; private set; } = GenerateUniqueID();
        public string Name { get; set; }
        public string Email { get; set; }

        private static Guid GenerateUniqueID()
        {
            return GuidUtilities.GenerateRandomGuid();
        }
    }
}