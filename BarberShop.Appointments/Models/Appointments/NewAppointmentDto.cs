using BarberShop.Appointments.Enums;

namespace BarberShop.Appointments.Models.Appointments;

public class NewAppointmentDto
{
    public string ClientId { get; set; }
    public string BarberId { get; set; }
    public DateTime ExactDate { get; set; }
    public IEnumerable<BarberServices> BarberServices { get; set; }
}