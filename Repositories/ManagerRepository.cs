namespace AirportTicketBooking.Repositories
{
    public class ManagerRepository
    {
        public Manager GetOrCreateManager(string name, string email, string password)
        {
            var existingManager = Program.managers.FirstOrDefault(p => p.Name == name && p.Email == email && p.Password == password);
            if (existingManager != null)
            {
                Console.WriteLine($"Welcome back, {existingManager.Name}!");
                return existingManager;
            }
            else
            {
                var newManager = new Manager
                {
                    Name = name,
                    Email = email,
                };
                newManager.Password = password;
                Program.managers.Add(newManager);
                Console.WriteLine($"Welcome, {newManager.Name}!");
                return newManager;
            }
        }
    }
}