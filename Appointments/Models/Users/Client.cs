namespace Appointments.Models.Users;

public class Client : User
{
    public Client()
    {
        SavedBarbers = new HashSet<Barber>();
    }

    public string Preferences { get; set; }
    public IEnumerable<Barber> SavedBarbers { get; set; }
}