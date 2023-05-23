using Appointments.Models.Users;

namespace Appointments.Services;

public interface IClientService
{
    public Client Get(string id);
}