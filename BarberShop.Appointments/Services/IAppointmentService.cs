using BarberShop.Appointments.Models;

namespace BarberShop.Appointments.Services;

public interface IAppointmentService
{
    public Appointment Get(string id);
    public Task<IEnumerable<Appointment>> GetAllAsync();
    public Task DeleteAsync(string id);
    public Task CreateAsync(Appointment appointment);
}