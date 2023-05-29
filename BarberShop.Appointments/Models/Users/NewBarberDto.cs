using BarberShop.Appointments.Enums;

namespace BarberShop.Appointments.Models.Users;

public class NewBarberDto : NewClientDto
{
    //TODO add right props

    public IEnumerable<BarberServices> AvailableServices { get; set; }
}