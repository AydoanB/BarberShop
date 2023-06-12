using BarberShop.Appointments.Enums;

namespace BarberShop.Appointments.Models.Users;

public class NewBarberDto : User
{
    public IEnumerable<BarberServices> AvailableServices { get; set; }
}