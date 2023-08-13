namespace AirportTicketBooking.Utilities
{
    public static class GuidUtilities
    {
        public static Guid GenerateRandomGuid()
        {
            return Guid.NewGuid();
        }

    }
}