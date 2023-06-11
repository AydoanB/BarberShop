using BarberShop.Appointments.Models.Appointments;

namespace BarberShop.Appointments.Services;

public interface IAppointmentService
{
    public Task<Appointment> GetAsync(string id);
    public Task<IEnumerable<Appointment>> GetAllAsync();
    public Task DeleteAsync(string id);
    public Task CreateAsync(NewAppointmentDto appointment);
}