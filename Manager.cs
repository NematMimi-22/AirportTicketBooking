using System;
namespace AirportTicketBooking
{
    public class Manager : User
    {
        private string Password;

        public string GetPassword
        {
            get { return Password; }
        }
        public void SetPassword(string password)
        {
            Password = password;
        }
    }
}