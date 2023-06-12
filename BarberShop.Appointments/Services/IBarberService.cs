using BarberShop.Appointments.Models.Users;

namespace BarberShop.Appointments.Services
{
    public interface IBarberService
    {
        public Task<Barber> GetAsync(string userId);
        public Task<IEnumerable<Barber>> GetAllAsync();
        public Task CreateAsync(NewBarberDto input, string currentUserId);
        public Task DeleteAsync(string userId);
        public Task CreateUser(NewBarberDto consumer);
    }
}
