using BarberShop.Identity.Data.Models;

namespace BarberShop.Identity.Services;

public interface IIdentityService
{
    Task<User> Register(UserInputModel userInput);

    Task<UserOutputModel> Login(UserInputModel userInput);

    Task ChangePassword(string userId, ChangePasswordInputModel changePasswordInput);
}