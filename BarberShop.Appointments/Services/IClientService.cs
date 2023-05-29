using BarberShop.Appointments.Models.Users;

namespace BarberShop.Appointments.Services;

public interface IClientService
{
    public Task<Client> GetAsync(string userId);
    public Task<IEnumerable<Client>> GetAllAsync();
    public Task CreateAsync(NewClientDto input, string currentUserId);
    public Task DeleteAsync(string userId);
}