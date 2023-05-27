using BarberShop.Identity.Data.Models;

namespace BarberShop.Identity.Services;

public interface ITokenGeneratorService
{
    string GenerateToken(User user);
}