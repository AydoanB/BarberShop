using Appointments.Models;

namespace Appointments.Services;

public interface IAppointmentService
{
    public Appointment Get(string id);
    public Task DeleteAsync(string id);
    public Task CreateAsync(Appointment appointment);
}