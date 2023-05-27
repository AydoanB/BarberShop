using Appointments.Models.Users;

namespace BarberShop.Appointments.Services;

public interface IClientService
{
    public Client Get(string id);
    public Task<string> CreateAsync(NewClientDto input);
    public Task DeleteAsync(string id);
}