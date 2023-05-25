using Appointments.Models.DTOs;
using Appointments.Models.Users;

namespace Appointments.Services;

public interface IClientService
{
    public Client Get(string id);
    public Task<string> CreateAsync(NewClientDto clientFromApi);
    public Task DeleteAsync(string id);
}