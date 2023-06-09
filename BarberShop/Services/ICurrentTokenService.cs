namespace BarberShop.Services;

public interface ICurrentTokenService
{
    string Get();
    void Set(string token);
}