using BarberShop.Identity.Data.Models;
using BarberShop.Identity.Models;
using BarberShop.Services;

namespace BarberShop.Identity.Services;

public interface IIdentityService
{
    Task<Result<User>> Register(UserInputModel userInput);

    Task<Result<UserOutputModel>> Login(UserInputModel userInput);

    Task<Result> ChangePassword(string userId, ChangePasswordInputModel changePasswordInput);
}